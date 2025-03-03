using CleanCQRS.Core.Dtos.DoctorService;

namespace CleanCQRS.Core.Interfaces
{
    public interface IDoctorServiceRepository
    {
        public Task<List<DoctorWithServicesDTO>> getDoctorWithServices();
    }
}
