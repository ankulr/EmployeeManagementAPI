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

            if(employee == null)
            {
                return BadRequest("Employee not found");
            }
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

    }
}
