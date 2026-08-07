using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       public  async Task<Employee> AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteAsync(Employee employee)
        {
             _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public Task<bool> DepartmentExistsAsync(int departmentid)
        {
            return _context.Departments.AnyAsync(e => e.DepartmentId == departmentid);
        }

        public async Task<bool> EmailExitsAsync(string email)
        {
            return await _context.Employees.AnyAsync(e => e.Email == email);

        }

        public async  Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.Include(e => e.Department).ToListAsync();
        }

     public  async  Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.AsNoTracking().Include(e => e.Department).FirstOrDefaultAsync(x => x.EmployeeId == id);
        }

      public  async  Task<Employee> UpdateAsync(Employee employee)
        {
             _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
    }
}
