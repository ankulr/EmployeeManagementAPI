namespace EmployeeManagement.DTOs.Common
{
    public class PaginationParamsDTO
    {
        public int PageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
