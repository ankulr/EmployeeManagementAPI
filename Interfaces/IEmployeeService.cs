using EmployeeManagement.DTOs;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeService
    {
     public    Task<EmployeeResponseDTO> CreateEmployeeAsync(EmployeeResponseDTO employeeDto);
     public    Task<EmployeeResponseDTO> GetEmployeeByIdAsync(int id);

     public   Task<IEnumerable<EmployeeResponseDTO>> GetAllEmployeesAsync();
     public   Task UpdateEmployeeAsync(int id, UpdateEmployeeDTO employeeDto);
     public    Task DeleteEmployeeAsync(int id);
    }
}
