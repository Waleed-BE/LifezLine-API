using CleanCQRS.Core.Dtos.DoctorService;
using CleanCQRS.Core.Interfaces;
using CleanCQRS.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace CleanCQRS.Infrastructure.Repository
{
    public class DoctorServiceRepository : IDoctorServiceRepository
    {
        private readonly AppDbContext _appDbContext;
        public DoctorServiceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<DoctorWithServicesDTO>> getDoctorWithServices()
        {
            var doctorsWithServices = await _appDbContext.TblDoctor
            .GroupBy(d => d.DoctorId)  // Group by DoctorId to ensure unique doctors
            .Select(g => new DoctorWithServicesDTO
            {
                DoctorId = g.Key,
                FirstName = g.FirstOrDefault().User.FirstName,  // Get the first doctor's first name
                LastName = g.FirstOrDefault().User.LastName,    // Get the first doctor's last name
                FullName = g.FirstOrDefault().User.FullName,    // Get the first doctor's full name
                Specialization = g.FirstOrDefault().Specialization,  // Get Specialization
                ProfessionalBackGround = g.FirstOrDefault().ProfessionalBackground,
                Certifications = g.FirstOrDefault().Certifications,
                ProfessionalMemberShip = g.FirstOrDefault().ProfessionMembership,
                Education = g.FirstOrDefault().Education,
                AppointmentFee = g.FirstOrDefault().AppointmentFee,
                Services = g.SelectMany(d => d.DoctorServices)
                             .Select(ds => ds.ProvidedService.ServiceName)  // Aggregate the service names
                             .Distinct() // Get distinct service names for each doctor
                             .ToList()
            })
            .ToListAsync();
            return doctorsWithServices;
        }
    }
}
