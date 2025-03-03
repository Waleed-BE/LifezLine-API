using CleanCQRS.Core.Entities;
using CleanCQRS.Core.Interfaces;
using CleanCQRS.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace CleanCQRS.Infrastructure.Repository
{
    public class EmployeeRepository(AppDbContext appDbContext) : IEmployeeRepository
    {
        public async Task<EmployeeEntity> AddEmployee(EmployeeEntity employeeEntity)
        {
            employeeEntity.EmployeeId = Guid.NewGuid();
            await appDbContext.Employees.AddAsync(employeeEntity);
            await appDbContext.SaveChangesAsync();
            return employeeEntity;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetAllEmployees()
        {
            return await appDbContext.Employees.ToListAsync();
        }
    }
}
