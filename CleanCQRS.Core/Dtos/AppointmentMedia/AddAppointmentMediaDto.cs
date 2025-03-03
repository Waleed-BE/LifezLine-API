namespace CleanCQRS.Core.Dtos.AppointmentMedia
{
    public class AddAppointmentMediaDto
    {
        public Guid AppointmentId { get; set; }
        public string AppointmentMediaPath { get; set; }
    }
}
