using CleanCQRS.Core.Dtos.Doctor;
using CleanCQRS.Core.Dtos.Services;
using CleanCQRS.Core.Interfaces;
using MediatR;

namespace CleanCQRS.Application.Command
{
    public record CreateDoctorCommand(AddDoctorDto AddDoctorDto) : IRequest<AddDoctorResponseDto>;
    public class CreateDoctorCommandHandler(IDoctorRepository doctorRepository,
                                            IProvidedServicesRepository providedServicesRepository,
        IMediator mediator) : IRequestHandler<CreateDoctorCommand, AddDoctorResponseDto>
    {
        public async Task<AddDoctorResponseDto> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            bool isUserCreated = await doctorRepository.createDoctor(request.AddDoctorDto);
            return new AddDoctorResponseDto()
            {   
                Message = "Registered Successfully"
            };
        }
    }
}
