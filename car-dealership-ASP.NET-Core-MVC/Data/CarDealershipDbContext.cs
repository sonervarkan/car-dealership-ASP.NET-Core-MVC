using Microsoft.EntityFrameworkCore;
using car_dealership_ASP.NET_Core_MVC.Models.Entities;

namespace car_dealership_ASP.NET_Core_MVC.Data
{
    public class CarDealershipDbContext: DbContext
    {
        public CarDealershipDbContext(DbContextOptions<CarDealershipDbContext>options): base(options) 
        {
            
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Images> Images { get; set; }



    }
}
