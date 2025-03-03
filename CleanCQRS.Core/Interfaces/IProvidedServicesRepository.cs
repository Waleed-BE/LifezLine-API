using CleanCQRS.Core.Dtos.Services;
using CleanCQRS.Core.Entities;

namespace CleanCQRS.Core.Interfaces
{
    public interface IProvidedServicesRepository
    {
        public Task<AddServiceResponseDTO> AddEditService(ServiceDTO addServiceDTO);
        public Task<List<ProvidedServices>> GetAllActiveService();
    }
}
