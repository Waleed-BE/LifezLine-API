using CleanCQRS.Core.Dtos.Doctor;

namespace CleanCQRS.Core.Interfaces
{
    public interface IDoctorScheduleRepository
    {
        public Task<List<DoctorAvailabilityDTO>> getDoctorAvailabilityAsync(Guid doctorId);
        public Task<List<DoctorAvailabilityDTO>> GetAllDoctorsScheduleAsync(DateTime startDate, DateTime endDate);
    }
}
