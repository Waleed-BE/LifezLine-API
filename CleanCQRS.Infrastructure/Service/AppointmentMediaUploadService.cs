using CleanCQRS.Core.IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCQRS.Infrastructure.Service
{
    public class AppointmentMediaUploadService : IAppointmentMediaUploadService
    {
        private readonly IWebHostEnvironment _env;

        public AppointmentMediaUploadService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> uploadMedia(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return "No file uploaded.";
            }

            // Ensure the uploads directory exists
            string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Generate a unique file name
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadsFolder, fileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the file path (can be a URL if serving static files)
            return $"uploads/{fileName}";
        }
    }
}
