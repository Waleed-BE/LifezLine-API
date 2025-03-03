using CleanCQRS.Application.Event.UserCreation;
using CleanCQRS.Core.Entities;
using CleanCQRS.Core.Interfaces;
using MediatR;

namespace CleanCQRS.Application.Command
{
    public record AddEmployeeCommand(EmployeeEntity EmployeeEntity) : IRequest<EmployeeEntity>;

    //#EventsLearn 3
    //Add IMediator in constructor #EventsLearn 3.1
    public class AddEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMediator mediator) : IRequestHandler<AddEmployeeCommand, EmployeeEntity>
    {
        public async Task<EmployeeEntity> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var user = await employeeRepository.AddEmployee(request.EmployeeEntity);
            //Add user mediator here #EventsLearn 3.2
            await mediator.Publish(new UserCreatedEvent(user.EmployeeId));
            return user;
        }
    }

}
