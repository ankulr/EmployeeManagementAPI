using EmployeeManagement.DTOs;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeService
    {
         Task<EmployeeResponseDTO> CreateEmployeeAsync(CreateEmployeeDTO  dto);
         Task<EmployeeResponseDTO> GetEmployeeByIdAsync(int id);

        Task<IEnumerable<EmployeeResponseDTO>> GetAllEmployeesAsync();
        Task<EmployeeResponseDTO> UpdateEmployeeAsync(int id, UpdateEmployeeDTO employeeDto);
        Task DeleteEmployeeAsync(int id);
    }
}
