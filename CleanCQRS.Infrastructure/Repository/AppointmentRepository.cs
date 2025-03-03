using CleanCQRS.Core.Dtos.Appointment;
using CleanCQRS.Core.Entities;
using CleanCQRS.Core.Interfaces;
using CleanCQRS.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace CleanCQRS.Infrastructure.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _appDbContext;

        public AppointmentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<AddAppointmentResponseDto> ScheduleAppointment(AddAppointmentDto appointmentCreateDto)
        {

            // If there is an appointment again on the same doctor's availability then don't allow to re-add that appointment.
            if (_appDbContext.TblAppointment.Where(x => x.AvailabilityId == appointmentCreateDto.availabilityId).Any())
            {
                return new AddAppointmentResponseDto()
                {
                    ReturnMessage = "This appointment is already present",
                    AppointmentId = Guid.Empty
                };
            }

            // Get the availability details against the availability Id.
            var availability = await _appDbContext.TblDoctorAvailability
                                        .Include(x => x.Doctor)
                                        .ThenInclude(x => x.User)
                                        .Where(x => x.AvailabilityId == appointmentCreateDto.availabilityId)
                                        .FirstOrDefaultAsync();

            // If the availability id does not exists then don't add the availability.
            if (availability == null)
            {
                return new AddAppointmentResponseDto()
                {
                    ReturnMessage = "The doctor availability is not present"
                };
            }

            // Create an appointment object.
            var appointment = new Appointment
            {
                AppointmentId = Guid.NewGuid(),
                AvailabilityId = appointmentCreateDto.availabilityId,
                PatientId = appointmentCreateDto.patientId,
            };

            await _appDbContext.TblAppointment.AddAsync(appointment);
            //await _appDbContext.SaveChangesAsync();

            return new AddAppointmentResponseDto() 
            {
                ReturnMessage = "Appointment Created successfully",
                AppointmentId = appointment.AppointmentId 
            };
        }

        public async Task<List<AppointmentDto>> GetAppointmentsByDoctor(Guid doctorId)
        {
            var appointments = await _appDbContext.TblAppointment
                .Include(a => a.DoctorAvailability)
                .ThenInclude(d => d.Doctor)
                .ThenInclude(d => d.User)
                .Include(a => a.Patient)
                .Include(a => a.Patient.User)
                .Where(a => a.DoctorAvailability.DoctorId == doctorId)
                .ToListAsync();

            List<AppointmentDto> appointmentDtos = new List<AppointmentDto>();
            foreach (var item in appointments)
            {
                AppointmentDto appointmentDto = new AppointmentDto();
                appointmentDto.AppointmentId = item.AppointmentId;
                appointmentDto.DoctorId = item.DoctorAvailability.DoctorId;
                appointmentDto.DoctorName = item.DoctorAvailability.Doctor.User.FirstName;
                appointmentDto.PatientId = item.PatientId;
                appointmentDto.PatientName = item.Patient.User.LastName;
                appointmentDto.SpecificDate = (DateTime)item.DoctorAvailability.SpecificDate;
                appointmentDto.DayOfWeek = (DayOfWeek)item.DoctorAvailability.DayOfWeek;
                appointmentDto.StartTime = item.DoctorAvailability.StartTime;
                appointmentDto.EndTime = item.DoctorAvailability.EndTime;
                appointmentDtos.Add(appointmentDto);
            }
            return appointmentDtos;

        }

        public async Task<List<AppointmentDto>> GetAppointmentsByPatient(Guid patientId)
        {
            var appointments = await _appDbContext.TblAppointment
                .Include(a => a.Patient)
                .ThenInclude(a => a.User)
                .Include(a => a.DoctorAvailability)
                .ThenInclude(d => d.Doctor)
                .ThenInclude(a => a.User)
                .Where(a => a.PatientId == patientId)
                .ToListAsync();

            List<AppointmentDto> appointmentDtos = new List<AppointmentDto>();
            foreach (var item in appointments)
            {
                AppointmentDto appointmentDto = new AppointmentDto();
                appointmentDto.AppointmentId = item.AppointmentId;
                appointmentDto.DoctorId = item.DoctorAvailability.DoctorId;
                appointmentDto.DoctorName = item.DoctorAvailability.Doctor.User.FirstName;
                appointmentDto.PatientId = item.PatientId;
                appointmentDto.PatientName = item.Patient.User.LastName;
                appointmentDto.SpecificDate = (DateTime)item.DoctorAvailability.SpecificDate;
                appointmentDto.DayOfWeek = (DayOfWeek)item.DoctorAvailability.DayOfWeek;
                appointmentDto.StartTime = item.DoctorAvailability.StartTime;
                appointmentDto.EndTime = item.DoctorAvailability.EndTime;
                appointmentDtos.Add(appointmentDto);
            }
            return appointmentDtos;
        }

        public async Task<bool> ApproveAppointmentByVerifier(Guid AppointmentId)
        {
            var appointmentToApprove = _appDbContext.TblAppointment
                .Where(x => x.AppointmentId == AppointmentId 
                         && x.Status == Core.Enums.AppointmentScheduleEnum.Pending).FirstOrDefault();

            if(appointmentToApprove == null)
            {
                return false;
            }

            appointmentToApprove.Status = Core.Enums.AppointmentScheduleEnum.Scheduled;

            _appDbContext.TblAppointment.Update(appointmentToApprove);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }

}
