using CleanCQRS.Core.Dtos.Appointment;
using CleanCQRS.Core.Interfaces;
using MediatR;

namespace CleanCQRS.Application.Query
{
    public record GetAppointmentByDoctorIdQuery(Guid DoctorId) : IRequest<List<AppointmentDto>>;
    public class GetAppointmentByDoctorIdQueryHandler(IAppointmentRepository appointmentRepository) : IRequestHandler<GetAppointmentByDoctorIdQuery, List<AppointmentDto>>
    {
        public async Task<List<AppointmentDto>> Handle(GetAppointmentByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            return await appointmentRepository.GetAppointmentsByDoctor(request.DoctorId);
        }
    }
}
