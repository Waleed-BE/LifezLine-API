using CleanCQRS.Core.Dtos.Doctor;
using CleanCQRS.Core.Interfaces;
using CleanCQRS.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;


namespace CleanCQRS.Infrastructure.Repository
{
    public class DoctorScheduleRepository : IDoctorScheduleRepository
    {
        private readonly AppDbContext _appDbContext;
        public DoctorScheduleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<DoctorAvailabilityDTO>> getDoctorAvailabilityAsync(Guid doctorId)
        {
              return await _appDbContext.TblDoctorAvailability
                .Where(da => da.DoctorId == doctorId)
                .Select(da => new DoctorAvailabilityDTO
                {
                    DoctorId = da.DoctorId,
                    DayOfWeek = da.DayOfWeek.HasValue ? da.DayOfWeek : null,
                    DoctorName = da.Doctor.User.FullName,
                    SpecificDate = da.SpecificDate,
                    StartTime = da.StartTime,
                    EndTime = da.EndTime,
                    IsRepeatable = da.IsRepeatable
                })
                .ToListAsync();
        }
        public async Task<List<DoctorAvailabilityDTO>> GetAllDoctorsScheduleAsync(DateTime startDate, DateTime endDate)
        {
            var availabilityList = new List<DoctorAvailabilityDTO>();

            // Fetch all doctors' availability records
            var allDoctorsAvailability = await _appDbContext.TblDoctorAvailability
                .Include(da => da.Doctor)
                .ThenInclude(d => d.User) // Include user data for doctor details
                .ToListAsync();

            for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                DayOfWeek currentDay = date.DayOfWeek;

                // Get specific date availabilities
                var specificAvailabilities = allDoctorsAvailability
                    .Where(da => !da.IsRepeatable && da.SpecificDate.HasValue && da.SpecificDate.Value.Date == date)
                    .ToList();

                // Get repeating weekly availabilities
                var repeatingAvailabilities = allDoctorsAvailability
                    .Where(da => da.IsRepeatable && da.DayOfWeek.HasValue && da.DayOfWeek.Value == currentDay)
                    .ToList();

                // Combine both specific and repeating availabilities
                var allAvailabilities = specificAvailabilities.Concat(repeatingAvailabilities);

                foreach (var availability in allAvailabilities)
                {
                    availabilityList.Add(new DoctorAvailabilityDTO
                    {
                        DoctorId = availability.DoctorId,
                        DoctorName = $"{availability.Doctor.User.FirstName} {availability.Doctor.User.LastName}",
                        DayOfWeek = availability.DayOfWeek,
                        StartTime = availability.StartTime,
                        EndTime = availability.EndTime,
                        IsRepeatable = availability.IsRepeatable
                    });
                }
            }

            return availabilityList;
        }



    }
}
