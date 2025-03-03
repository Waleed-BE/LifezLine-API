

namespace CleanCQRS.Core.Dtos.DoctorService
{
    public class DoctorWithServicesDTO
    {
        public Guid DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public string ProfessionalBackGround { get; set; }
        public string Certifications { get; set; }
        public string ProfessionalMemberShip { get; set; }
        public string Education { get; set; }
        public decimal AppointmentFee { get; set; }
        public List<string> Services { get; set; }
    }
}
