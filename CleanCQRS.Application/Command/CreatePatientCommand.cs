using CleanCQRS.Core.Dtos.Patient;
using CleanCQRS.Core.Interfaces;
using MediatR;

namespace CleanCQRS.Application.Command
{
    public record CreatePatientCommand(AddPatientDto AddPatientDto) : IRequest<AddPatientResponseDto>;
    public class CreatePatientCommandHandler(IPatientRepository PatientRepository, IMediator mediator) : IRequestHandler<CreatePatientCommand, AddPatientResponseDto>
    {
        public async Task<AddPatientResponseDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            return await PatientRepository.AddEditPatient(request.AddPatientDto);
        }
    }
}
