using CleanCQRS.Core.Entities;
using CleanCQRS.Core.Interfaces;
using MediatR;

namespace CleanCQRS.Application.Query
{
    public record GetAllProvidedServicesQuery() : IRequest<List<ProvidedServices>>;
    public class GetAllProvidedServicesQueryHandler(IProvidedServicesRepository providedServicesRepository) : IRequestHandler<GetAllProvidedServicesQuery, List<ProvidedServices>>
    {
        public async Task<List<ProvidedServices>> Handle(GetAllProvidedServicesQuery request, CancellationToken cancellationToken)
        {
            return await providedServicesRepository.GetAllActiveService();
        }
    }
}
