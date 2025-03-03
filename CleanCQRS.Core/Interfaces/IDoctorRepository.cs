using CleanCQRS.Core.Dtos.Doctor;

namespace CleanCQRS.Core.Interfaces
{
    public interface IDoctorRepository
    {
        public Task<bool> createDoctor(AddDoctorDto addDoctorDto);
        public Task<DoctorAvailabilityResponseDTO> createDoctorAvailability(List<DoctorAvailabilityDTO> doctorAvailabilityDTOs);
        public  Task<bool> updateDoctorAvailability(DoctorAvailabilityDTO doctorAvailability);
    }
}
