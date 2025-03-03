using CleanCQRS.Core.Dtos.AppointmentMedia;

namespace CleanCQRS.Core.Interfaces
{
    public interface IAppointmentMediaRepository
    {
        public Task<AddAppointmentMediaResponseDto> AddAppointmentMedia(AddAppointmentMediaDto addAppointmentMediaDto);
    }
}
