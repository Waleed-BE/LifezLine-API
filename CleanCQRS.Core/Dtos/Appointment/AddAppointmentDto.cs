using Microsoft.AspNetCore.Http;

namespace CleanCQRS.Core.Dtos.Appointment
{
    public class AddAppointmentDto
    {
        public Guid availabilityId { get; set; }
        public Guid patientId { get; set; }
        public DateTime? appointmentDate { get; set; }
        public TimeSpan? startTime { get; set; }
        public TimeSpan? endTime { get; set; }
        public IFormFile mediaFile { get; set; }
    }
}
