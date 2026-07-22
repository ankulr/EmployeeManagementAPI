using EmployeeManagement.Models;
using System.Collections;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();
       Task<Employee> UpdateAsync(Employee employee);
         Task<bool> DeleteAsync(Employee employee);
        Task<bool> EmailExitsAsync(string email);

        Task<bool> DepartmentExists(int departmentid);


    }
}
