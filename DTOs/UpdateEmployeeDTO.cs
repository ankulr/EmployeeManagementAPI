using EmployeeManagement.Models;

namespace EmployeeManagement.DTOs
{
    public class UpdateEmployeeDTO
    {
        public decimal Salary { get; set; }
        public string Name { get; set; }

       public int DepartmentId { get; set; }
    }
}
