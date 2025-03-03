using CleanCQRS.Core.Dtos.Appointment;

namespace CleanCQRS.Core.Interfaces
{
    public interface IAppointmentRepository
    {
        public Task<AddAppointmentResponseDto> ScheduleAppointment(AddAppointmentDto addAppointmentDto);
        public Task<List<AppointmentDto>> GetAppointmentsByDoctor(Guid doctorId);
        public Task<List<AppointmentDto>> GetAppointmentsByPatient(Guid patientId);
        public Task<bool> ApproveAppointmentByVerifier(Guid AppointmentId);

    }
}
