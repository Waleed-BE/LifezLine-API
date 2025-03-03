namespace CleanCQRS.Core.Dtos.Services
{
    public class ServiceDTO
    {
        public Guid? ServiceId { get; set; }
        public string ServiceName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
