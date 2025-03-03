using CleanCQRS.Core.Dtos.Appointment;
using CleanCQRS.Core.Interfaces;
using MediatR;

namespace CleanCQRS.Application.Query
{

    public record GetAppointmentByPatientIdQuery(Guid PatientId) : IRequest<List<AppointmentDto>>;
    public class GetAppointmentByPatientIdQueryHandler(IAppointmentRepository appointmentRepository) : IRequestHandler<GetAppointmentByPatientIdQuery, List<AppointmentDto>>
    {
        public async Task<List<AppointmentDto>> Handle(GetAppointmentByPatientIdQuery request, CancellationToken cancellationToken)
        {
            return await appointmentRepository.GetAppointmentsByPatient(request.PatientId);
        }
    }
}
