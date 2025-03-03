using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanCQRS.Core.Entities
{
    public class Doctor
    {
        [Key]
        public Guid DoctorId { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public decimal AppointmentFee { get; set; }
        public string Education { get; set; }
        public string ProfessionalBackground { get; set; }
        public string Specialization { get; set; }
        public string Certifications { get; set; }
        public string PMCNumber { get; set; }
        public string PracticingTenure { get; set; }
        public string ProfessionMembership { get; set; }
        public DateTime DOB { get; set; }
        public User User { get; set; }
        public ICollection<DoctorService> DoctorServices { get; set; }
    }
}
