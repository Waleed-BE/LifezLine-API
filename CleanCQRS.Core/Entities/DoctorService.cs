using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanCQRS.Core.Entities
{
    public class DoctorService
    {
        [Key]
        public Guid DoctorServiceId { get; set; }

        [ForeignKey(nameof(Doctor))]
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [ForeignKey(nameof(ProvidedServices))]
        public Guid ServiceId { get; set; }
        public ProvidedServices ProvidedService { get; set; }

    }

}
