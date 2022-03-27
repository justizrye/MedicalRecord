using MedicalRecord.Entity;
using MedicalRecord.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Web;

namespace MedicalRecord.WebAPI.Controllers
{
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository employeeRepo, ILogger<EmployeeController> logger)
        {
            _employeeRepo = employeeRepo;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into EmployeeController");
        }

        [HttpGet("/api/employees")]
        public IActionResult GetAllEmployees()
        {
            _logger.LogInformation("GetAllEmployees Start");
            try
            {
                var employees = _employeeRepo.GetAllEmployees();
                _logger.LogInformation("GetAllEmployees End");
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogInformation("GetAllEmployees End");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/api/employees-async")]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            _logger.LogInformation("GetAllEmployeesAsync Start");
            try
            {
                var employees = await _employeeRepo.GetAllEmployeesAsync();
                _logger.LogInformation("GetAllEmployeesAsync End");
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogInformation("GetAllEmployeesAsync End");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/api/search-employees/id={id}&firstName={firstName}&lastName={lastName}&updatedDateStart={updatedDateStart}&updatedDateEnd={updatedDateEnd}&temperatureFrom={temperatureFrom}&temperatureTo={temperatureTo}")]
        public IActionResult SearchEmployees(int id, string firstName, string lastName, DateTime updatedDateStart, DateTime updatedDateEnd, int temperatureFrom, int temperatureTo)
        {
            _logger.LogInformation("SearchEmployees Start");
            try
            {
                firstName = HttpUtility.HtmlDecode(firstName);
                if (firstName == "''" || firstName == "\"\"")
                {
                    firstName = String.Empty;
                }
                
                lastName = HttpUtility.HtmlDecode(lastName);
                if (lastName == "''" || lastName == "\"\"")
                { 
                    lastName = String.Empty; 
                }
                
                
                if (id < 0 && string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && updatedDateStart == DateTime.MinValue && updatedDateEnd == DateTime.MinValue && temperatureFrom < 0 && temperatureTo < 0)
                {
                    Exception ex = new Exception("Missing search criteria");
                    _logger.LogError(ex, ex.Message);
                    _logger.LogInformation("SearchEmployeesAsync End");
                    return BadRequest(ex);
                }
                //if (updatedDateStart == DateTime.MinValue || updatedDateEnd == DateTime.MinValue)
                //{
                //    return BadRequest(new Exception("Valid date range must be specified"));
                //}
                //if (temperatureFrom < 0 || temperatureTo < 0)
                //{
                //    return BadRequest(new Exception("Valid temperature range must be specified"));
                //}
                var employees = _employeeRepo.SearchEmployees(id, firstName, lastName, updatedDateStart, updatedDateEnd, temperatureFrom, temperatureTo);
                _logger.LogInformation("SearchEmployees End");
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogInformation("SearchEmployees End");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/api/search-employees-async/id={id}&firstName={firstName}&lastName={lastName}&updatedDateStart={updatedDateStart}&updatedDateEnd={updatedDateEnd}&temperatureFrom={temperatureFrom}&temperatureTo={temperatureTo}")]
        public async Task<IActionResult> SearchEmployeesAsync(int id, string firstName, string lastName, DateTime updatedDateStart, DateTime updatedDateEnd, int temperatureFrom, int temperatureTo)
        {
            _logger.LogInformation("SearchEmployeesAsync Start");
            try
            {
                firstName = HttpUtility.HtmlDecode(firstName);
                if (firstName == "''" || firstName == "\"\"")
                {
                    firstName = String.Empty;
                }

                lastName = HttpUtility.HtmlDecode(lastName);
                if (lastName == "''" || lastName == "\"\"")
                {
                    lastName = String.Empty;
                }


                if (id < 0 && string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && updatedDateStart == DateTime.MinValue && updatedDateEnd == DateTime.MinValue && temperatureFrom < 0 && temperatureTo < 0)
                {
                    Exception ex = new Exception("Missing search criteria");
                    _logger.LogError(ex, ex.Message);
                    _logger.LogInformation("SearchEmployeesAsync End");
                    return BadRequest(ex);
                }
                //if (updatedDateStart == DateTime.MinValue || updatedDateEnd == DateTime.MinValue)
                //{
                //    return BadRequest(new Exception("Valid date range must be specified"));
                //}
                //if (temperatureFrom < 0 || temperatureTo < 0)
                //{
                //    return BadRequest(new Exception("Valid temperature range must be specified"));
                //}
                var employees = await _employeeRepo.SearchEmployeesAsync(id, firstName, lastName, updatedDateStart, updatedDateEnd, temperatureFrom, temperatureTo);
                _logger.LogInformation("SearchEmployeesAsync End");
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogInformation("SearchEmployeesAsync End");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/api/employees/{id}")]
        public IActionResult GetEmployee(int id)
        {
            _logger.LogInformation("GetEmployee Start");
            try
            {
                var employee = _employeeRepo.GetEmployee(id);
                if (employee == null) return NotFound();

                _logger.LogInformation("GetEmployee End");
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogInformation("GetEmployee End");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/api/employees-async/{id}")]
        public async Task<IActionResult> GetEmployeeAsync(int id)
        {
            _logger.LogInformation("GetEmployeeAsync Start");
            try
            {
                var employee = await _employeeRepo.GetEmployeeAsync(id);
                if (employee == null) return NotFound();

                _logger.LogInformation("GetEmployeeAsync End");
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogInformation("GetEmployeeAsync End");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/api/employees-async")]
        public async Task<IActionResult> CreateEmployeeAsync(Employee employee)
        {
            _logger.LogInformation("CreateEmployeeAsync Start");
            try
            {
                var createdEmployee = await _employeeRepo.CreateEmployeeAsync(employee);
                _logger.LogInformation("CreateEmployeeAsync End");
                return CreatedAtAction(nameof(GetEmployeeAsync), "Employee", new { id = createdEmployee.Id.ToString("N") }, createdEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogInformation("CreateEmployeeAsync End");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("/api/employees")]
        public IActionResult CreateEmployee(Employee employee)
        {
            _logger.LogInformation("CreateEmployee Start");
            try
            {
                var createdEmployee = _employeeRepo.CreateEmployee(employee);
                _logger.LogInformation("CreateEmployee End");
                return CreatedAtAction(nameof(GetEmployee), "Employee", new { id = createdEmployee.Id.ToString("N") }, createdEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogInformation("CreateEmployee End");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("/api/employees-async/{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(int id, Employee employee)
        {
            _logger.LogInformation("UpdateEmployeeAsync Start");
            try
            {
                var existingEmployee = await _employeeRepo.GetEmployeeAsync(id);
                if (existingEmployee == null)
                    return NotFound();

                await _employeeRepo.UpdateEmployeeAsync(id, employee);
                _logger.LogInformation("UpdateEmployeeAsync End");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogInformation("UpdateEmployeeAsync End");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("/api/employees/{id}")]
        public IActionResult UpdateEmployee(int id, Employee employee)
        {
            _logger.LogInformation("UpdateEmployee Start");
            try
            {
                var existingEmployee = _employeeRepo.GetEmployee(id);
                if (existingEmployee == null)
                    return NotFound();

                _employeeRepo.UpdateEmployee(id, employee);
                _logger.LogInformation("UpdateEmployee End");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogInformation("UpdateEmployee End");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("/api/employees-async/{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            _logger.LogInformation("DeleteEmployeeAsync Start");
            try
            {
                var existingEmployee = await _employeeRepo.GetEmployeeAsync(id);
                if (existingEmployee == null)
                    return NotFound();

                await _employeeRepo.DeleteEmployeeAsync(id);
                _logger.LogInformation("DeleteEmployeeAsync End");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogInformation("DeleteEmployeeAsync End");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("/api/employees/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _logger.LogInformation("DeleteEmployee Start");
            try
            {
                var existingEmployee = _employeeRepo.GetEmployee(id);
                if (existingEmployee == null)
                    return NotFound();

                _employeeRepo.DeleteEmployee(id);
                _logger.LogInformation("DeleteEmployee End");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogInformation("DeleteEmployee End");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
