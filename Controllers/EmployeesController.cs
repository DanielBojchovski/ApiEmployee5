using ApiEmployee5.Models;
using ApiEmployee5.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEmployee5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _empRepository;
        public EmployeesController(IEmployeeRepository empRepository)
        {
            _empRepository = empRepository;
        }

        [HttpGet("GetAverageSalary", Name = "Print average salary in the company./ Печати просечна плата во компанијата.")]
        public decimal GetAverageSalary()
        {
            return _empRepository.GetAverageSalary();
        }

        [HttpGet("GetTotalSumOfSalaries", Name = "Print sum of all salaries in the company./ Печати ја сумата од платите во компанијата.")]
        public decimal GetTotalSumOfSalaries()
        {
            return _empRepository.GetTotalSumOfSalaries();
        }

        [HttpGet("GetSalariesBiggerThen/{salary:decimal}", Name = "Search the employees with salary bigger than your value./ Пребарај ги вработените со плата поголема од вашата вредност.")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetSalariesBiggerThen(decimal salary)
        {
            try
            {
                var result = await _empRepository.GetSalariesBiggerThen(salary);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound($"No employee with salary bigger than {salary} was not found./ Нема вработен со плата поголема од {salary}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database./ Грешка при превземањето на податоци од базата");
            }
        }

        [HttpGet("SearchByName/{name}", Name = "Search the employee by name./ Пребарај го вработениот по име.")]
         public async Task<ActionResult<IEnumerable<Employee>>> SearchByName(string name)
         {
             try
             {
                 var result = await _empRepository.SearchByName(name);
                 if (result.Any())
                 {
                     return Ok(result);
                 }
                 return NotFound($"Employee with name = {name} was not found./ Вработен со име = {name} не беше пронајден");
             }
             catch (Exception)
             {
                 return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database./ Грешка при превземањето на податоци од базата");
             }
         }

        [HttpGet("GetSalariesLessThen/{salary:decimal}", Name = "Search the employees with salary smaller than your value./ Пребарај ги вработените со плата помала од вашата вредност.")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetSalariesLessThen(decimal salary)
        {
            try
            {
                var result = await _empRepository.GetSalariesLessThen(salary);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound($"No employee with salary smaller than {salary} was not found./ Нема вработен со плата помала од {salary}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database./ Грешка при превземањето на податоци од базата");
            }
        }

        [HttpGet("SearchBySkill/{skill}", Name = "Search the employees by skill./ Пребарај ги вработените по вештина.")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchBySkill(string skill)
        {
            try
            {
                var result = await _empRepository.SearchBySkill(skill);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound($"Employee with skill = {skill} was not found./ Вработен со вештина = {skill} не беше пронајден");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database./ Грешка при превземањето на податоци од базата");
            }
        }

        [HttpGet("GetEmployees", Name = "Print all employees./ Печати ги сите вработени.")]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _empRepository.Get());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database./ Грешка при превземањето на податоци од базата");
            }
        }

        [HttpGet("GetEmployee/{id:int}", Name = "Get employee by id./ Пребарај вработен по идентификационен број.")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await _empRepository.Get(id);

                if (result == null)
                {
                    return NotFound($"Employee with id = {id} does not exist./ Вработен со идентификационен број = {id} не постои.");
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database./ Грешка при превземањето на податоци од базата");
            }
        }

        [HttpPost("PostEmployees", Name = "Enter Employee./ Внеси вработен.")]
        public async Task<ActionResult<Employee>> PostEmployees(Employee employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest();

                var emp = await _empRepository.GetEmployeeByEmail(employee.Email);

                if (emp != null)
                {
                    ModelState.AddModelError("Email", "Employee email already in use./ Веќе имаме вработен со оваа email адреса");
                    return BadRequest(ModelState);
                }

                var createdEmployee = await _empRepository.Create(employee);

                return CreatedAtAction(nameof(GetEmployee),
                    new { id = createdEmployee.EmployeeId }, createdEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record./ Грешка при креирање нов вработен.");
            }
        }

        [HttpPut("UpdateEmployee/{id:int}", Name = "Update employee./ Измени податоци за вработен.")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                    return BadRequest("Employee ID mismatch./ Неусогласеност помеѓу индентификациониот број и вработениот");

                var employeeToUpdate = await _empRepository.Get(id);

                if (employeeToUpdate == null)
                {
                    return NotFound($"Employee with Id = {id} not found./ Вработен со број = {id} не беше пронајден");
                }

                return await _empRepository.Update(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating employee record./ Грешка при ажурирање на вработениот");
            }
        }

        [HttpDelete("DeleteEmployee/{id:int}", Name = "Delete employee./ Избриши вработен.")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await _empRepository.Get(id);

                if (employeeToDelete == null)
                {
                    return NotFound($"Employee with Id = {id} not found./ Вработен со број = {id} не беше пронајден");
                }

                await _empRepository.Delete(id);

                return Ok($"Employee with Id = {id} deleted./ Вработен со број = {id} е избришан");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record. No employee was deleted./ Грешка при бришење на вработен. Ниту еден вработен не беше избришан");
            }
        }
    }
}
