using EmployeeManagement.DTOs;
using EmployeeManagement.Exceptions;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository , ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        public async Task<EmployeeResponseDTO> CreateEmployeeAsync(CreateEmployeeDTO dto)
        {
            _logger.LogInformation("Creating employee with email {Email}", dto.Email);
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new BadRequestException("Employee name is required");
            }
            if(string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new InvalidOperationException("Email is required");
            }
            if(dto.Salary <= 0)
            {
                throw new BadRequestException("salary must be greater than zero");
            }

            // Duplicate email
            bool emailexists =  await  _employeeRepository.EmailExitsAsync(dto.Email);
            if(emailexists)
            {
                _logger.LogWarning("Employee creation failed.Email {Email} already exists");
               
                throw new ConflictException("Email alreday exists");
            }

            // Department exists

            bool departmentExists = await _employeeRepository.DepartmentExistsAsync(dto.DepartmentId);  

            if(!departmentExists)
            {
                _logger.LogWarning("Employee creation failed.Department {DepartmentId} does not exist", dto.DepartmentId);
                throw new BadRequestException("Department doesnot exist");
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

            _logger.LogInformation("Employee created successfully with id {EmployeedID}", savedEmployee.EmployeeId);

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
            _logger.LogInformation("Deleting employee with id {EmployeeId}", id);
            Employee employee = await _employeeRepository.GetByIdAsync(id);

            if(employee == null)
            {
                _logger.LogWarning("Delete failed. Employee {EmployeeId} not found", id);
                throw new NotFoundException("Employee not found");
            }

            await _employeeRepository.DeleteAsync(employee);
            _logger.LogInformation("Employee {EmployeeId} deleted successfully", id);
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

        public async Task<EmployeeResponseDTO> GetEmployeeByIdAsync(int id)
        {
            _logger.LogInformation("fetching Employee with {EmployeeId}", id);
            var employee = await _employeeRepository.GetByIdAsync(id);

            if(employee == null)
            {
                _logger.LogWarning("Employee with id {EmployeeId} was not found", id);

                throw new NotFoundException("Employee was not found with this id");
            }

            _logger.LogInformation("Employee with id {EmployeeId} was found successfully", id);

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
            _logger.LogInformation("Updating employee with id {EmployeeId}", id);
            var employee = await _employeeRepository.GetByIdAsync(id);

            if(employee == null)
            {
                _logger.LogWarning("Update failed Employee {EmployeeId} not found", id);
                throw new NotFoundException("Employee not found");
            }

            /// salary check
                if(dto .Salary <=0)
            {
                throw new BadRequestException("Salary must be greater than zero");

            }

            // department check
            if(! await _employeeRepository.DepartmentExistsAsync(dto.DepartmentId))
            {
                _logger.LogWarning("Update failed.Department {DepartmentId} does nopt exists");
                throw new BadRequestException("Department already exists");
            }

            if(dto.Email != employee.Email && await _employeeRepository.EmailExitsAsync(dto.Email))
            {
                _logger.LogWarning("Update failed. Email {Email} already exist", dto.Email);
                throw new ConflictException("Email already exists");
            }

            // updating the employee entity

            employee.Name = dto.Name;
            employee.Salary = dto.Salary;
            employee.DepartmentId = dto.DepartmentId;
            employee.Email = dto.Email;

            // Save Entity 

            Employee updatedEmployee = await _employeeRepository.UpdateAsync(employee);

            _logger.LogInformation("Employee {EmployeeId} updated successfully", updatedEmployee.EmployeeId);

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
