namespace EmployeeManagement.DTOs.Common
{
    public class EmployeeSearchDTO
    {
        public int? DepartmentId { get; set; }
        public string? SortBY { get; set; }
        public string? SortOrder { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
