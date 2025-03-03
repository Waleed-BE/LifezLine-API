using CleanCQRS.Core.Dtos.Doctor;
using CleanCQRS.Core.Entities;
using CleanCQRS.Core.Enums;
using CleanCQRS.Core.Interfaces;
using CleanCQRS.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace CleanCQRS.Infrastructure.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _appDbContext;
        public DoctorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<bool> createDoctor(AddDoctorDto addDoctorDto)
        {
            if (addDoctorDto.IsEdit)
            {
                //get user by id
                var userById = _appDbContext.TblUser.Where(x => x.UserId == addDoctorDto.UserId).FirstOrDefault();

                if (userById == null)
                {
                    return false;
                }

                //get doctor by id
                var doctorByUserId = _appDbContext.TblDoctor.Where(x => x.UserId == addDoctorDto.UserId).FirstOrDefault();


                //update user
                userById.City = addDoctorDto.City;
                userById.FullName = addDoctorDto.FullName;
                userById.FirstName = addDoctorDto.FirstName;
                userById.LastName = addDoctorDto.LastName;
                userById.PhoneNumber = addDoctorDto.PhoneNumber;
                userById.OTP = addDoctorDto.Otp;
                userById.OTPGeneratedUTCTime = DateTime.UtcNow;

                //update doctor
                doctorByUserId.AppointmentFee = addDoctorDto.AppointmentFee;
                doctorByUserId.Certifications = addDoctorDto.Certifications;
                doctorByUserId.DOB = addDoctorDto.DOB;
                doctorByUserId.Education = addDoctorDto.Education;
                doctorByUserId.PMCNumber = addDoctorDto.PMCNumber;
                doctorByUserId.PracticingTenure = addDoctorDto.PracticingTenure;
                doctorByUserId.ProfessionalBackground = addDoctorDto.ProfessionalBackground;
                doctorByUserId.ProfessionMembership = addDoctorDto.ProfessionMembership;
                doctorByUserId.Specialization = addDoctorDto.Specialization;



                //save changes
                _appDbContext.TblUser.Update(userById);
                _appDbContext.TblDoctor.Update(doctorByUserId);
                await _appDbContext.SaveChangesAsync();

                // return response.
                return true;
            }

            //Create User with role doctor.
            User user = new User()
            {
                UserId = Guid.NewGuid(),
                RoleId = _appDbContext.Set<Role>().Where(x => x.RoleName == RoleEnum.doctor.ToString()).Select(x => x.Id).FirstOrDefault(),
                PhoneNumber = addDoctorDto.PhoneNumber,
                OTP = addDoctorDto.Otp,
                OTPGeneratedUTCTime = DateTime.UtcNow,
                IsOtpVerified = false,
                City = addDoctorDto.City,
                FullName = addDoctorDto.FullName,
                FirstName = addDoctorDto.FirstName,
                LastName = addDoctorDto.LastName
            };

            //Create Doctor with the id of the user.
            Doctor doctor = new Doctor()
            {
                DoctorId = Guid.NewGuid(),
                AppointmentFee = addDoctorDto.AppointmentFee,
                Certifications = addDoctorDto.Certifications,
                DOB = addDoctorDto.DOB,
                Education = addDoctorDto.Education,
                PMCNumber = addDoctorDto.PMCNumber,
                PracticingTenure = addDoctorDto.PracticingTenure,
                ProfessionalBackground = addDoctorDto.ProfessionalBackground,
                ProfessionMembership = addDoctorDto.ProfessionMembership,
                Specialization = addDoctorDto.Specialization,
                UserId = user.UserId,
            };


            //The list of services will be received in the form of GUID
            List<DoctorService> doctorServicesEntityList = new List<DoctorService>();
            if (addDoctorDto.ProvidedServices.Length > 0)
            {
                foreach (var serviceItem in addDoctorDto.ProvidedServices)
                {
                    DoctorService doctorServiceEntity = new DoctorService();
                    doctorServiceEntity.DoctorServiceId = Guid.NewGuid();
                    doctorServiceEntity.ServiceId = serviceItem;
                    doctorServiceEntity.DoctorId = doctor.DoctorId;
                    bool doctorService = _appDbContext.TblDoctorService.Where(x => x.DoctorId == doctor.DoctorId && x.ServiceId == serviceItem).Any();
                    if (doctorService == false)
                    {
                        doctorServicesEntityList.Add(doctorServiceEntity);
                    }
                }
                await _appDbContext.TblDoctorService.AddRangeAsync(doctorServicesEntityList);
            }
            //Insert to db.
            await _appDbContext.TblUser.AddAsync(user);
            await _appDbContext.TblDoctor.AddAsync(doctor);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<DoctorAvailabilityResponseDTO> createDoctorAvailability(List<DoctorAvailabilityDTO> doctorAvailabilityDTOs)
        {
            var doctorAvailabilityList = new List<DoctorAvailability>();
            foreach (var item in doctorAvailabilityDTOs)
            {
                DoctorAvailability doctorAvailability = new DoctorAvailability()
                {
                    AvailabilityId = Guid.NewGuid(),
                    DoctorId = item.DoctorId,
                    DayOfWeek = item.DayOfWeek,  // Weekly repeating availability
                    SpecificDate = item.SpecificDate,
                    StartTime = item.StartTime, //new TimeSpan(9, 0, 0),  // 09:00 AM break it to hours and minutes
                    EndTime = item.EndTime,//new TimeSpan(17, 0, 0),  // 05:00 PM
                    IsRepeatable = item.IsRepeatable
                };
                doctorAvailabilityList.Add(doctorAvailability);
            }
            await _appDbContext.TblDoctorAvailability.AddRangeAsync(doctorAvailabilityList);
            await _appDbContext.SaveChangesAsync();
            return new DoctorAvailabilityResponseDTO()
            {
                ResponseMessage = "Schedule created successfully"
            };
        }
        public async Task<bool> updateDoctorAvailability(DoctorAvailabilityDTO doctorAvailability)
        {
            var existingRecord = await _appDbContext.TblDoctorAvailability
                .FirstOrDefaultAsync(d => d.DoctorId == doctorAvailability.DoctorId &&
                                          d.DayOfWeek == doctorAvailability.DayOfWeek &&
                                          d.SpecificDate == doctorAvailability.SpecificDate &&
                                          d.StartTime == doctorAvailability.StartTime &&
                                          d.EndTime == doctorAvailability.EndTime);

            if (existingRecord != null)
            {
                existingRecord.IsRepeatable = doctorAvailability.IsRepeatable; // Update repeatable flag
                _appDbContext.TblDoctorAvailability.Update(existingRecord);
                await _appDbContext.SaveChangesAsync();
                return true; // Return true indicating update success
            }

            return false; // Return false if no record was found
        }


    }
}
