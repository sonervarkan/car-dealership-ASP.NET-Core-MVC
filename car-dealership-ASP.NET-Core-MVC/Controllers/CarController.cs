using car_dealership_ASP.NET_Core_MVC.Data;
using car_dealership_ASP.NET_Core_MVC.Models;
using car_dealership_ASP.NET_Core_MVC.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace car_dealership_ASP.NET_Core_MVC.Controllers
{
    public class CarController : Controller
    {
        private readonly CarDealershipDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CarController(CarDealershipDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
        public IActionResult AddCar(CarViewModel model)
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

                // Cars tablosu
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
                _context.SaveChanges();

                // Images tablosuna ImgUrl ve wwwroot klasörüne resim kaydetme
                if (model.CarImages != null && model.CarImages.Count > 0)
                {
                    string uploadPath = Path.Combine(_env.WebRootPath, "images", "cars"); // wwwroot/images/cars/ oluşturacak

                    // klasör yoksa oluştur
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    foreach (var image in model.CarImages)
                    {
                        if (image.Length > 0)
                        {
                            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                            string filePath = Path.Combine(uploadPath, uniqueFileName);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                image.CopyTo(fileStream);
                            }

                            // Veritabanına kaydet
                            var imgEntity = new Images
                            {
                                ImgUrl = "/images/cars/" + uniqueFileName,
                                CarId = newCar.Id
                            };

                            _context.Images.Add(imgEntity);
                        }
                    }

                    _context.SaveChanges();
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Hata oluştu: " + ex.Message);
                return View(model);
            }
        }


    }
}
