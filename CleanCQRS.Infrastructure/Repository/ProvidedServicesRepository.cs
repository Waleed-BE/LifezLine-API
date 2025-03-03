using CleanCQRS.Core.Dtos.Services;
using CleanCQRS.Core.Entities;
using CleanCQRS.Core.Interfaces;
using CleanCQRS.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace CleanCQRS.Infrastructure.Repository
{
    public class ProvidedServicesRepository : IProvidedServicesRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProvidedServicesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<AddServiceResponseDTO> AddEditService(ServiceDTO addServiceDTO)
        {
            if(addServiceDTO.ServiceId == null)
            {
                //Add
                var service = _appDbContext.TblProvidedServices.Where(x => x.ServiceName.ToLower().Equals(addServiceDTO.ServiceName)).Any();
                if (service)
                {
                    return new AddServiceResponseDTO() { ResponseMessage= "Service already exists" };
                }

                ProvidedServices providedServices = new ProvidedServices()
                {
                    ServiceId = Guid.NewGuid(),
                    ServiceName = addServiceDTO.ServiceName,
                    IsActive = true                    
                };

                await _appDbContext.AddAsync(providedServices);
                await _appDbContext.SaveChangesAsync();
                return new AddServiceResponseDTO() { ResponseMessage = "Service Added Successfully" };

            }
            else
            {
                //Edit
                //Add
                var service = _appDbContext.TblProvidedServices.Where(x => x.ServiceId == addServiceDTO.ServiceId && x.IsActive == true).FirstOrDefault();
                if (service != null)
                {
                    service.ServiceName = addServiceDTO.ServiceName;
                    service.IsActive = true;
                    _appDbContext.TblProvidedServices.Update(service);
                    await _appDbContext.SaveChangesAsync();
                    return new AddServiceResponseDTO() { ResponseMessage = "Service updated successfully." };

                }else
                {
                    return new AddServiceResponseDTO() { ResponseMessage = "Service you are trying to edit does not exists." };
                }
            }
        }

        public async Task<List<ProvidedServices>> GetAllActiveService()
        {
            return await _appDbContext.TblProvidedServices.Where(x => x.IsActive == true).ToListAsync();
        }
    }
}
