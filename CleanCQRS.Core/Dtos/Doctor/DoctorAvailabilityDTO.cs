namespace CleanCQRS.Core.Dtos.Doctor
{
    public class DoctorAvailabilityDTO
    {
        public Guid DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public DayOfWeek? DayOfWeek { get; set; } // For repeating schedules
        public DateTime? SpecificDate { get; set; } // For specific days
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsRepeatable { get; set; }
    }

}
