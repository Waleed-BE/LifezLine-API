namespace CleanCQRS.Core.Dtos.Doctor
{
    public class AddDoctorDto
    {
        public Guid UserId { get; set; }
        public decimal AppointmentFee { get; set; }
        public string Education { get; set; }
        public string ProfessionalBackground { get; set; }
        public string Specialization { get; set; }
        public string Certifications { get; set; }
        public Guid[] ProvidedServices { get; set; }
        public string PMCNumber { get; set; }
        public string PracticingTenure { get; set; }
        public string ProfessionMembership { get; set; }
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }
        public string? Otp { get; set; }
        public string City { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsEdit { get; set; }
    }
}
