using CleanCQRS.Core.Interfaces;
using MediatR;

namespace CleanCQRS.Application.Command
{
    public record CreateAppointmentVerificationCommand(Guid AppointmentId) : IRequest<bool>;
    public class CreateAppointmentVerificationHandler(IAppointmentRepository appointmentRepository, IMediator mediator) : IRequestHandler<CreateAppointmentVerificationCommand, bool>
    {
        public async Task<bool> Handle(CreateAppointmentVerificationCommand request, CancellationToken cancellationToken)
        {
            return await appointmentRepository.ApproveAppointmentByVerifier(request.AppointmentId);
        }
    }
}
