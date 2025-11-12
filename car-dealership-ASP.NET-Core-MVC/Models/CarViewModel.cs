using car_dealership_ASP.NET_Core_MVC.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace car_dealership_ASP.NET_Core_MVC.Models
{
    public class CarViewModel
    {
        public Cars? CarVMAdd { get; set; }
        public List<Users>? Users { get; set; }

        //  Resim yükleme alanı
        public List<IFormFile>? CarImages { get; set; }

        // FILTER  
        public List<string>? Brands { get; set; } // Bu liste kullanılacak
        public List<string>? Models { get; set; }

    }
}
