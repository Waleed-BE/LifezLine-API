using System.ComponentModel.DataAnnotations;

namespace CleanCQRS.Core.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; }
        public string PhoneNumber { get; set; }
        public string OTP { get; set; }
        public DateTime? OTPGeneratedUTCTime { get; set; }
        public bool IsOtpVerified { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? AddressLine { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsLocked { get; set; } = false;
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }

    }

}
