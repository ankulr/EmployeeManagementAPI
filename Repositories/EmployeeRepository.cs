using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.Data;

namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       public  Task<Employee> AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

     public    Task<bool> DeleteAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DepartmentExists(int departmentid)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailExitsAsync(string email)
        {
            throw new NotImplementedException();
        }

        public  Task<IEnumerable<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

     public    Task<Employee> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

      public   Task<Employee> UpdateAsync(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
