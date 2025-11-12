namespace car_dealership_ASP.NET_Core_MVC.Models.Entities
{
    public class Roles
    {
        public int Id { get; set; }
        public string RoleType { get; set; }= string.Empty;

        // Navigation property (1 role -> many users)
        public ICollection<Users> Users { get; set; } = new List<Users>();

    }
}
