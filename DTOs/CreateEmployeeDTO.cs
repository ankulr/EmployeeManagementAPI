namespace EmployeeManagement.DTOs
{
    public class CreateEmployeeDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}
