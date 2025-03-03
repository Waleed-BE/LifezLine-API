using CleanCQRS.Core.Dtos.Doctor;
using CleanCQRS.Core.Interfaces;
using MediatR;

namespace CleanCQRS.Application.Query
{
    public record GetAllDoctorScheduleQuery(DateTime startDate, DateTime endDate) : IRequest<List<DoctorAvailabilityDTO>>;
    public class GetAllDoctorScheduleQueryHandler(IDoctorScheduleRepository doctorScheduleRepository) : IRequestHandler<GetAllDoctorScheduleQuery, List<DoctorAvailabilityDTO>>
    {
        public async Task<List<DoctorAvailabilityDTO>> Handle(GetAllDoctorScheduleQuery request, CancellationToken cancellationToken)
        {
            return await doctorScheduleRepository.GetAllDoctorsScheduleAsync(request.startDate, request.endDate);
        }
    }
}