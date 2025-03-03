using System.ComponentModel.DataAnnotations;

namespace CleanCQRS.Core.Entities
{
    public class ProvidedServices
    {
        [Key]
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public bool IsActive { get; set; }
        public ICollection<DoctorService> DoctorServices { get; set; }

    }

}
