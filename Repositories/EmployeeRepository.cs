using ApiEmployee5.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEmployee5.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;
        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }
        public async Task<Employee> Create(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task Delete(int employeeId)
        {
            var employeeToDelete = await _context.Employees.FindAsync(employeeId);
            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> Get(int employeeId)
        {
            return await _context.Employees.FindAsync(employeeId);
        }

        public decimal GetAverageSalary()
        {
            decimal sum = 0;
            int length = _context.Employees.Count();
            foreach (var employee in _context.Employees)
            {
                sum += employee.Salary;
            }
            return sum / length;
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Employee>> GetSalariesBiggerThen(decimal salary)
        {
            IQueryable<Employee> query = _context.Employees;

            query = query.Where(e => e.Salary > salary);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetSalariesLessThen(decimal salary)
        {
            IQueryable<Employee> query = _context.Employees;

            query = query.Where(e => e.Salary < salary);

            return await query.ToListAsync();
        }

        public decimal GetTotalSumOfSalaries()
        {
            decimal sum = 0;
            foreach (var employee in _context.Employees)
            {
                sum += employee.Salary;
            }
            return sum;
        }

        public async Task<IEnumerable<Employee>> SearchByName(string name)
        {
            IQueryable<Employee> query = _context.Employees;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.FirstName.Contains(name)
                            || e.LastName.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> SearchBySkill(string skill)
        {
            IQueryable<Employee> query = _context.Employees;

            if (!string.IsNullOrEmpty(skill))
            {
                query = query.Where(e => e.Skills.Contains(skill));
            }
            return await query.ToListAsync();
        }

        public async Task<Employee> Update(Employee employee)
        {
            var result = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.Salary = employee.Salary;
                result.Email = employee.Email;
                result.DateOfBrith = employee.DateOfBrith;
                result.Gender = employee.Gender;
                result.Skills = employee.Skills;
                if (employee.DepartmentId != 0)
                {
                    result.DepartmentId = employee.DepartmentId;
                }
                else if (employee.Department != null)
                {
                    result.DepartmentId = employee.Department.DepartmentId;
                }
                result.PhotoPath = employee.PhotoPath;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
