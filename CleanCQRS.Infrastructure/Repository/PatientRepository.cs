using CleanCQRS.Core.Dtos.Patient;
using CleanCQRS.Core.Entities;
using CleanCQRS.Core.Enums;
using CleanCQRS.Core.Interfaces;
using CleanCQRS.Infrastructure.ApplicationDbContext;


namespace CleanCQRS.Infrastructure.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _appDbContext;
        public PatientRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<AddPatientResponseDto> AddEditPatient(AddPatientDto addPatient)
        {
            if (addPatient.IsEdit)
            {
                //get user by id
                var userById = _appDbContext.TblUser.Where(x => x.UserId == addPatient.UserId).FirstOrDefault();

                if (userById == null)
                {
                    return new AddPatientResponseDto() { ReturnMessage = "Patient Not Found" };
                }

                //get patient by id
                var patientByUserId = _appDbContext.TblPatient.Where(x => x.UserId == addPatient.UserId).FirstOrDefault();


                //update user

                userById.FirstName = addPatient.FirstName;
                userById.LastName = addPatient.LastName;
                userById.PhoneNumber = addPatient.PhoneNumber;
                userById.OTP = addPatient.OTP;
                userById.OTPGeneratedUTCTime = DateTime.UtcNow;

                //update patient
                patientByUserId.PatientDescription = addPatient.PatientDescription;

                //save changes
                _appDbContext.TblUser.Update(userById);
                _appDbContext.TblPatient.Update(patientByUserId);
                await _appDbContext.SaveChangesAsync();

                // return response.
                return new AddPatientResponseDto() { ReturnMessage = "Patient Updated Successfully" };
            }

            //Create User with role patient.
            User user = new User()
            {
                UserId = Guid.NewGuid(),
                RoleId = _appDbContext.Set<Role>().Where(x => x.RoleName == RoleEnum.patient.ToString()).Select(x => x.Id).FirstOrDefault(),
                PhoneNumber = addPatient.PhoneNumber,
                OTP = addPatient.OTP,
                OTPGeneratedUTCTime = DateTime.UtcNow,
                IsOtpVerified = false,
                FirstName = addPatient.FirstName,
                LastName = addPatient.LastName
            };

            //Create Patient with the id of the user.
            Patient patient = new Patient()
            {
                PatientId = Guid.NewGuid(),
                PatientDescription = String.Empty,
                UserId = user.UserId,
            };



            //Insert to db.
            await _appDbContext.TblUser.AddAsync(user);
            await _appDbContext.TblPatient.AddAsync(patient);
            await _appDbContext.SaveChangesAsync();


            return new AddPatientResponseDto() { ReturnMessage = "Patient Added Successfully." };
        }
    }
}
