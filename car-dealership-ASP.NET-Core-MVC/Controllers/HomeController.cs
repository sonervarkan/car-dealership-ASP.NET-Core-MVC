using car_dealership_ASP.NET_Core_MVC.Data;
using car_dealership_ASP.NET_Core_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace car_dealership_ASP.NET_Core_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly CarDealershipDbContext _context;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env, CarDealershipDbContext context)
        {
            _logger = logger;
            _env = env;
            _context = context;
        }

        public IActionResult Index()
        {
            // wwwroot/images/cars
            string folderPath = Path.Combine(_env.WebRootPath, "images", "cars");

            // Take jpg/jpeg/PNG files
            var imageFiles = Directory.Exists(folderPath)
                ? Directory.GetFiles(folderPath, "*.*")
                    .Where(f => f.EndsWith(".jpg") || f.EndsWith(".jpeg") || f.EndsWith(".PNG"))
                    .Select(f => "/images/cars/" + Path.GetFileName(f))
                    .ToList()
                : new List<string>();

            ViewBag.Images = imageFiles;

            // FILTER
            var vm = new CarViewModel();

            vm.Brands = _context.Cars
                                .Select(c => c.Brand)
                                .Distinct()
                                .ToList();
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // -------------------------FILTER---------------------------------
        // ------------GET MODELS--------------
        [HttpGet]
        public IActionResult GetModelsByBrand(string brandSelected)
        {
            var models = _context.Cars
                             .Where(c => c.Brand == brandSelected)
                             .Select(c => c.Model)
                             .Distinct()
                             .ToList();
            return Json(models);
        }

        // ------------GET GEAR TYPES-------------
        [HttpGet]
        public IActionResult GetGearTypesByModelBrand(string brandSelected, string modelSelected) 
        {
            var gearTypes = _context.Cars
                            .Where(c => c.Brand == brandSelected && c.Model == modelSelected)
                            .Select(c => c.GearType)
                            .Distinct()
                            .ToList();
            return Json(gearTypes);
        }

        [HttpGet]
        public IActionResult GetFuelTypesByBrandModelGearType(string brandSelected, string modelSelected, string gearTypeSelected)
        {
            var fuelTypes = _context.Cars
                            .Where(c => c.Brand == brandSelected && c.Model == modelSelected && c.GearType == gearTypeSelected)
                            .Select(c => c.FuelType)
                            .Distinct()
                            .ToList();
            return Json(fuelTypes);
        }

        // ---------SEARCH BUTTON---------------

        [HttpGet]
        public IActionResult Filter(
            string? brandSelected,
            string? modelSelected,
            string? gearTypeSelected,
            string? fuelTypeSelected,
            int? yearMin,
            int? yearMax,
            decimal? priceMin,
            decimal? priceMax)
        {
            string sql = @"
                SELECT * FROM Cars
                WHERE Brand = @brand
                  AND Model = @model
                  AND GearType = @gear
                  AND FuelType = @fuel
                  AND Year BETWEEN @yearMin AND @yearMax
                  AND Price BETWEEN @priceMin AND @priceMax";

            var parameters = new[]
            {
                new SqlParameter("@brand", brandSelected),
                new SqlParameter("@model", modelSelected),
                new SqlParameter("@gear", gearTypeSelected),
                new SqlParameter("@fuel", fuelTypeSelected),
                new SqlParameter("@yearMin", yearMin ?? 0),
                new SqlParameter("@yearMax", yearMax ?? 9999),
                new SqlParameter("@priceMin", priceMin ?? 0),
                new SqlParameter("@priceMax", priceMax ?? 999999999)
            };

            var filteredCars = _context.Cars
                .FromSqlRaw(sql, parameters)
                .Include(c => c.Images)
                .ToList();



            ViewBag.FilteredCars = filteredCars;


            var vm = new CarViewModel
            {
                Brands = _context.Cars.Select(c => c.Brand).Distinct().ToList()
            };

            return View("Filter", vm);
        }


    }
}
