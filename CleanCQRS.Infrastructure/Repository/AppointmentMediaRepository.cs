using CleanCQRS.Core.Dtos.AppointmentMedia;
using CleanCQRS.Core.Entities;
using CleanCQRS.Core.Interfaces;
using CleanCQRS.Infrastructure.ApplicationDbContext;

namespace CleanCQRS.Infrastructure.Repository
{
    public class AppointmentMediaRepository : IAppointmentMediaRepository
    {
        private readonly AppDbContext _appDbContext;
        public AppointmentMediaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<AddAppointmentMediaResponseDto> AddAppointmentMedia(AddAppointmentMediaDto addAppointmentMediaDto)
        {
            AppointmentMedia appointmentMedia = new AppointmentMedia()
            {
                AppointmentMediaId = Guid.NewGuid(),
                AppointmentId = addAppointmentMediaDto.AppointmentId,
                MediaPath = addAppointmentMediaDto.AppointmentMediaPath
            };

            await _appDbContext.TblAppointmentMedia.AddAsync(appointmentMedia);
            await _appDbContext.SaveChangesAsync();
            return new AddAppointmentMediaResponseDto()
            {
                ReturnMessage = "Appointment Media Added."
            };
        }
    }
}
