using car_dealership_ASP.NET_Core_MVC.Data;
using car_dealership_ASP.NET_Core_MVC.Models;
using car_dealership_ASP.NET_Core_MVC.Models.Entities;
using car_dealership_ASP.NET_Core_MVC.Services; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // For Session 
using System.IO; // For Path 

namespace car_dealership_ASP.NET_Core_MVC.Controllers
{
    public class CarController : Controller
    {
        private readonly CarDealershipDbContext _context;

        // private readonly IWebHostEnvironment _env; // Now it is unnecessary

        private readonly ICloudinaryService _cloudinaryService; 

        
        public CarController(CarDealershipDbContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService; 
            // _env removed
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            return View(new CarViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(CarViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    ModelState.AddModelError("", "Lütfen giriş yapın.");
                    return View(model);
                }

                // 1. Cars table
                var newCar = new Cars
                {
                    Brand = model.CarVMAdd.Brand,
                    Model = model.CarVMAdd.Model,
                    GearType = model.CarVMAdd.GearType,
                    FuelType = model.CarVMAdd.FuelType,
                    Year = model.CarVMAdd.Year,
                    Color = model.CarVMAdd.Color,
                    Price = model.CarVMAdd.Price,
                    UserId = userId.Value
                };

                _context.Cars.Add(newCar);
                await _context.SaveChangesAsync();

                // 2. saving Cloudinary URL to Images table
                if (model.CarImages != null && model.CarImages.Count > 0)
                {
                    // wwwroot koding is removed

                    foreach (var image in model.CarImages)
                    {
                        if (image.Length > 0)
                        {
                            // CLOUDINARY INSTALLATION PROCESS
                            var uploadResult = await _cloudinaryService.UploadImageAsync(image);

                            // if success
                            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var imgEntity = new Images
                                {
                                    
                                    ImgUrl = uploadResult.SecureUrl.ToString(),
                                    PublicId = uploadResult.PublicId, // keep the PublicId in case the image is deleted
                                    CarId = newCar.Id
                                };

                                _context.Images.Add(imgEntity);
                            }
                            else
                            {
                                
                                ModelState.AddModelError("", $"Image could not be loaded: {uploadResult?.Error?.Message ?? "unknown error"}");

                                // If you need to delete your Car record in case of an error
                                // _context.Cars.Remove(newCar);
                                // await _context.SaveChangesAsync();

                                return View(model);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
                return View(model);
            }
        }
    }
}