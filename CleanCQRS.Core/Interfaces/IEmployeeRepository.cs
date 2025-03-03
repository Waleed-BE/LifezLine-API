using CleanCQRS.Core.Entities;

namespace CleanCQRS.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<EmployeeEntity>> GetAllEmployees();
        public Task<EmployeeEntity> AddEmployee(EmployeeEntity employeeEntity);
    }
}
