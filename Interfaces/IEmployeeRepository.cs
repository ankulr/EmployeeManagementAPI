using EmployeeManagement.DTOs.Common;
using EmployeeManagement.Models;
using System.Collections;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddAsync(Employee employee);
        Task<Employee> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();
       Task<Employee> UpdateAsync(Employee employee);
         Task  DeleteAsync(Employee employee);
        Task<bool> EmailExitsAsync(string email);

        Task<bool> DepartmentExistsAsync(int departmentid);
        Task<IEnumerable<Employee>> GetPagedEmployeeAsync(int pageNUmber, int pageSize);

        Task<IEnumerable<Employee>> searchEmployeeAsync(EmployeeSearchDTO searchDTO);


    }
}
