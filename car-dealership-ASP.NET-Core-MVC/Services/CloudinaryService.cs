using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration; // AppSettings ve Secrets'i okumak için gerekli

namespace car_dealership_ASP.NET_Core_MVC.Services
{
   
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

       
        public CloudinaryService(IConfiguration config)
        {
            // Read settings from Secrets.json
            var cloudName = config["CloudinarySettings:CloudName"];
            var apiKey = config["CloudinarySettings:ApiKey"];
            var apiSecret = config["CloudinarySettings:ApiSecret"];

            // Create an account for Cloudinary
            var account = new Account(cloudName, apiKey, apiSecret);

            // Launching the Cloudinary client
            _cloudinary = new Cloudinary(account);
        }

        // ICloudinaryService
        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "car-dealership" 
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        // Delete by publicId
        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}