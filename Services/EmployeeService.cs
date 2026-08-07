using EmployeeManagement.DTOs;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

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

        public Task DeleteEmployeeAsync(int id)
        {
            throw new NotImplementedException();
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

        public Task<EmployeeResponseDTO> UpdateEmployeeAsync(int id, UpdateEmployeeDTO employeeDto)
        {
            throw new NotImplementedException();
        }
    }
}
