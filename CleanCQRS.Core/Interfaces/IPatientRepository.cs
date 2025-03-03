using CleanCQRS.Core.Dtos.Patient;

namespace CleanCQRS.Core.Interfaces
{
    public interface IPatientRepository
    {
        public Task<AddPatientResponseDto> AddEditPatient(AddPatientDto addPatient);
    }
}
