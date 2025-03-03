namespace CleanCQRS.Core.Dtos.Appointment
{
    public class AddAppointmentResponseDto
    {
        public string ReturnMessage { get; set; }
        public Guid AppointmentId { get; set; }
    }
}
