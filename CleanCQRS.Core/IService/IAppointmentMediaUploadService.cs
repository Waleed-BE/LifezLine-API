using Microsoft.AspNetCore.Http;

namespace CleanCQRS.Core.IService
{
    public interface IAppointmentMediaUploadService
    {
        Task<string> uploadMedia(IFormFile formFile);
    }
}
