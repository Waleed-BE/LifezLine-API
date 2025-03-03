using CleanCQRS.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanCQRS.Core.Entities
{
    public class Appointment
    {
        [Key]
        public Guid AppointmentId { get; set; }
        [ForeignKey(nameof(DoctorAvailability))]
        public Guid AvailabilityId { get; set; }
        public DoctorAvailability DoctorAvailability { get; set; }
        [ForeignKey(nameof(Patient))]
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public AppointmentScheduleEnum Status { get; set; } 
        public string? Notes { get; set; } // Optional notes from doctor or patient
    }
}
