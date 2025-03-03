namespace CleanCQRS.Core.Dtos.OTP
{
    public class VerifyOtpDto
    {
        public DateTime UTCDatetime { get; set; }
        public string OTP { get; set; }
        public string PhoneNumber { get; set; }
    }
}
