using EmployeeManagement.DTOs;
using EmployeeManagement.DTOs.Common;
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


        [HttpGet]

        public async Task<ActionResult<IEnumerable<EmployeeResponseDTO>>> GetAll()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponseDTO>> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

           
            return Ok(employee);
        }


        [HttpPost]
        public async Task<ActionResult<EmployeeResponseDTO>>  Create(CreateEmployeeDTO dto)
        {
            var employee = await _employeeService.CreateEmployeeAsync(dto);

            return Ok(employee);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeResponseDTO>> Update(int id, UpdateEmployeeDTO dto)
        {
            var employee = await _employeeService.UpdateEmployeeAsync(id, dto);

            if (employee == null)
            {
                return BadRequest("Employee Not Updated");
            }

            return Ok("updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpGet("paged")]
        
        public async Task<ActionResult<IEnumerable<EmployeeResponseDTO>>> GetPagedEmployees([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var employees = await _employeeService.GetPagedEmployeeAsync(pageNumber, pageSize);

            return Ok(employees);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<EmployeeResponseDTO>>> SearchEmployees([FromQuery] EmployeeSearchDTO searchDTO)
        {
            var employees = await _employeeService.SearchEmployeeAsync(searchDTO);
            return Ok(employees);
        }

    }
}
