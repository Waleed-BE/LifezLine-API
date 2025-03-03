using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanCQRS.Core.Entities
{
    public class DoctorAvailability
    {
        [Key]
        public Guid AvailabilityId { get; set; }

        [ForeignKey(nameof(Doctor))]
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DayOfWeek? DayOfWeek { get; set; } // If repeating availability (e.g., every Monday)
        public DateTime? SpecificDate { get; set; } // If non-repeating (e.g., only on 15th Feb)
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsRepeatable { get; set; } // True for weekly availability, False for specific dates
    }
}
