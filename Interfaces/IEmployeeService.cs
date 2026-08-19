using EmployeeManagement.DTOs;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeService
    {
         Task<EmployeeResponseDTO> CreateEmployeeAsync(CreateEmployeeDTO  dto);
         Task<EmployeeResponseDTO> GetEmployeeByIdAsync(int id);

        Task<IEnumerable<EmployeeResponseDTO>> GetAllEmployeesAsync();
        Task<EmployeeResponseDTO> UpdateEmployeeAsync(int id, UpdateEmployeeDTO dto);
        Task DeleteEmployeeAsync(int id);
        Task<IEnumerable<EmployeeResponseDTO>> GetPagedEmployeeAsync(int pageNumber, int pageSize);
    }
}
