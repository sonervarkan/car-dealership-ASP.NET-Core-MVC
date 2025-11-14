
using CloudinaryDotNet.Actions; 
using Microsoft.AspNetCore.Http;

namespace car_dealership_ASP.NET_Core_MVC.Services
{
   
    public interface ICloudinaryService
    {
        
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);

        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}
