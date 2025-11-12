using car_dealership_ASP.NET_Core_MVC.Models.Entities;

namespace car_dealership_ASP.NET_Core_MVC.Models
{
    public class RegisterViewModel
    {
        public Users ?UserVMAdd { get; set; }
        public List<Roles> ?Roles { get; set; }
    }
}
