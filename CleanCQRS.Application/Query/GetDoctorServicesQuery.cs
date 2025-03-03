
using CleanCQRS.Core.Dtos.DoctorService;
using CleanCQRS.Core.Interfaces;
using MediatR;

namespace CleanCQRS.Application.Query
{
    public record GetDoctorServicesQuery() : IRequest<List<DoctorWithServicesDTO>>;
    public class GetDoctorServicesQueryHandler(IDoctorServiceRepository doctorServiceRepository) : IRequestHandler<GetDoctorServicesQuery, List<DoctorWithServicesDTO>>
    {
        public async Task<List<DoctorWithServicesDTO>> Handle(GetDoctorServicesQuery request, CancellationToken cancellationToken)
        {
            return await doctorServiceRepository.getDoctorWithServices();
        }
    }
}
