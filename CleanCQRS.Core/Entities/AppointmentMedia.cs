using System.ComponentModel.DataAnnotations.Schema;

namespace CleanCQRS.Core.Entities
{
    public class AppointmentMedia
    {
        public Guid AppointmentMediaId { get; set; }
        public string MediaPath { get; set; }
        [ForeignKey(nameof(Appointment))]
        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
