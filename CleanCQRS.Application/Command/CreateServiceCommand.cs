using CleanCQRS.Core.Dtos.Services;
using CleanCQRS.Core.Interfaces;
using MediatR;

namespace CleanCQRS.Application.Command
{
    public record CreateServiceCommand(ServiceDTO ServiceDto) : IRequest<AddServiceResponseDTO>;
    public class AddServiceCommandHandler(IProvidedServicesRepository providedServicesRepository, IMediator mediator) : IRequestHandler<CreateServiceCommand, AddServiceResponseDTO>
    {
        public async Task<AddServiceResponseDTO> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            return await providedServicesRepository.AddEditService(request.ServiceDto);
        }
    }
}
