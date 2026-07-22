namespace EmployeeManagement.DTOs
{
    public class EmployeeResponseDTO
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
       public string DepartmentName { get; set; }
    }
}
