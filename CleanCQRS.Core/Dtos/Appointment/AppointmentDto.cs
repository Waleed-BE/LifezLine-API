namespace CleanCQRS.Core.Dtos.Appointment
{
    public class AppointmentDto
    {
        public Guid AppointmentId { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; }
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public DayOfWeek DayOfWeek { get; set; } 
        public DateTime SpecificDate { get; set; } 
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
