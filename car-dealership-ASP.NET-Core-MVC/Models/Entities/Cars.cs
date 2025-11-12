namespace car_dealership_ASP.NET_Core_MVC.Models.Entities
{
    public class Cars
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string GearType {  get; set; } = string.Empty;
        public string FuelType {  get; set; } = string.Empty;
        public int Year { get; set; }
        public string Color {  get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;


        public int UserId { get; set; }
        public Users? User { get; set; }

        public ICollection<Images> Images { get; set; } = new List<Images>();

    }
}
