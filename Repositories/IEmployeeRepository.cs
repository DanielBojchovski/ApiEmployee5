using ApiEmployee5.Models;

namespace ApiEmployee5.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> SearchByName(string name);
        Task<IEnumerable<Employee>> SearchBySkill(string skill);
        Task<IEnumerable<Employee>> Get();
        Task<Employee> Get(int employeeId);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<Employee> Create(Employee employee);
        Task<Employee> Update(Employee employee);
        Task Delete(int employeeId);
        decimal GetAverageSalary();
        decimal GetTotalSumOfSalaries();
        Task<IEnumerable<Employee>> GetSalariesBiggerThen(decimal salary);
        Task<IEnumerable<Employee>> GetSalariesLessThen(decimal salary);
    }
}
