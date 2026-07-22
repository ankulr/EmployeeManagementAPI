using EmployeeManagement.DTOs;
using EmployeeManagement.Interfaces;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        public Task<EmployeeResponseDTO> CreateEmployeeAsync(EmployeeResponseDTO employeeDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEmployeeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeResponseDTO>> GetAllEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeResponseDTO> GetEmployeeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmployeeAsync(int id, UpdateEmployeeDTO employeeDto)
        {
            throw new NotImplementedException();
        }
    }
}
