using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CleanCQRS.Core.Entities
{
    public class Patient
    {

        [Key]
        public Guid PatientId { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public string PatientDescription { get; set; }
        public User User { get; set; }

    }
}
