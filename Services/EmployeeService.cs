using EmployeeManagement.DTOs;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeResponseDTO> CreateEmployeeAsync(CreateEmployeeDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new Exception("Employee name is required");
            }
            if(string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new InvalidOperationException("Email is required");
            }
            if(dto.Salary <= 0)
            {
                throw new Exception("salary must be greater than zero");
            }

            // Duplicate email
            bool emailexists =  await  _employeeRepository.EmailExitsAsync(dto.Email);
            if(emailexists)
            {
                throw new Exception("Email alreday exists");
            }

            // Department exists

            bool departmentExists = await _employeeRepository.DepartmentExistsAsync(dto.DepartmentId);  

            if(!departmentExists)
            {
                throw new Exception("Department doesnot exist");
            }

            // Entity mapping dto to employee

            Employee employee = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                Salary = dto.Salary,
                DepartmentId = dto.DepartmentId,
            };

            // Saving the employee 

            Employee savedEmployee = await _employeeRepository.AddAsync(employee);

            // Entity mapping from Employee to dto

            EmployeeResponseDTO response = new EmployeeResponseDTO
            {
                EmployeeId = savedEmployee.EmployeeId,
                Name = savedEmployee.Name,
                Salary = savedEmployee.Salary,
                DepartmentId = savedEmployee.DepartmentId

            };

            return response;

        }

        public async Task DeleteEmployeeAsync(int id)
        {
            Employee employee = await _employeeRepository.GetByIdAsync(id);

            if(employee == null)
            {
                throw new Exception("Employee not found");
            }

            await _employeeRepository.DeleteAsync(employee);
        }

        public async Task<IEnumerable<EmployeeResponseDTO>> GetAllEmployeesAsync()
        {
            // Get all the employees
            IEnumerable<Employee> employees = await _employeeRepository.GetAllAsync();

            // Create response list 

            List<EmployeeResponseDTO> employeeResponsedto = new List<EmployeeResponseDTO>();

            // mapping each employee to the employeeResponse list

            foreach(Employee employee in employees)
            {
                EmployeeResponseDTO response = new EmployeeResponseDTO()
                {
                    EmployeeId= employee.EmployeeId,
                    Name = employee.Name,
                    Salary =  employee.Salary,
                    DepartmentId = employee.DepartmentId,
                    DepartmentName = employee.Department.DepartmentName
                };
                employeeResponsedto.Add(response);
            }

            // returning employee response list

            return employeeResponsedto;


        }

        public async Task<EmployeeResponseDTO?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if(employee == null)
            {
                return null;
            }

            EmployeeResponseDTO responseDTO = new EmployeeResponseDTO
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department.DepartmentName
            };

            return responseDTO;
        }

        public async Task<EmployeeResponseDTO> UpdateEmployeeAsync(int id, UpdateEmployeeDTO dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if(employee == null)
            {
                throw new Exception("Employee not found");
            }

            /// salary check
                if(dto .Salary <=0)
            {
                throw new Exception("Salary must be greater than zero");

            }

            // department check
            if(! await _employeeRepository.DepartmentExistsAsync(dto.DepartmentId))
            {
                throw new Exception("Department already exists");
            }

            if(dto.Email != employee.Email && await _employeeRepository.EmailExitsAsync(dto.Email))
            {
                throw new Exception("Email already exists");
            }

            // updating the employee entity

            employee.Name = dto.Name;
            employee.Salary = dto.Salary;
            employee.DepartmentId = dto.DepartmentId;
            employee.Email = dto.Email;

            // Save Entity 

            Employee updatedEmployee = await _employeeRepository.UpdateAsync(employee);

            // Updated employee to response dto

            return new EmployeeResponseDTO
            {
                EmployeeId = updatedEmployee.EmployeeId,
                Name = updatedEmployee.Name,
                Salary = updatedEmployee.Salary,
                Email = updatedEmployee.Email,
                DepartmentId = updatedEmployee.DepartmentId,
                DepartmentName = updatedEmployee.Department?.DepartmentName

            };


        }
    }
}
