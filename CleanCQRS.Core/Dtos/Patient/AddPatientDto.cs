namespace CleanCQRS.Core.Dtos.Patient
{
    public class AddPatientDto
    {
        public Guid? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PatientDescription { get; set; }
        public string OTP { get; set; }
        public bool IsEdit { get; set; } = false;
    }
}
