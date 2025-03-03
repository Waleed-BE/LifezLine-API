using CleanCQRS.Core.Dtos.Doctor;
using CleanCQRS.Core.Interfaces;
using MediatR;

namespace CleanCQRS.Application.Query
{
    public record GetDoctorScheduleQuery(Guid DoctorId) : IRequest<List<DoctorAvailabilityDTO>>;
    public class GetDoctorScheduleQueryHandler(IDoctorScheduleRepository doctorScheduleRepository) : IRequestHandler<GetDoctorScheduleQuery, List<DoctorAvailabilityDTO>>
    {
        public async Task<List<DoctorAvailabilityDTO>> Handle(GetDoctorScheduleQuery request, CancellationToken cancellationToken)
        {
            return await doctorScheduleRepository.getDoctorAvailabilityAsync(request.DoctorId);
        }
    }
}
