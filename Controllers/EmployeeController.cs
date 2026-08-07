using EmployeeManagement.DTOs;
using EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeResponseDTO>>  Create(CreateEmployeeDTO dto)
        {
            var employee = await _employeeService.CreateEmployeeAsync(dto);

            return Ok(employee);

        }


    }
}
