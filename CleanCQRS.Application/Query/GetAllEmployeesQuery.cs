using CleanCQRS.Core.Entities;
using CleanCQRS.Core.Interfaces;
using MediatR;

namespace CleanCQRS.Application.Query
{
    public record GetAllEmployeesQuery() : IRequest<IEnumerable<EmployeeEntity>>;
    public class GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeEntity>>
    {
        public async Task<IEnumerable<EmployeeEntity>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await employeeRepository.GetAllEmployees();
        }
    }
}
