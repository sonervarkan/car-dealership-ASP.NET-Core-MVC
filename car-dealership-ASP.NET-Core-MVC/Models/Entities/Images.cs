namespace car_dealership_ASP.NET_Core_MVC.Models.Entities
{
    public class Images
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; } = string.Empty;


        public int CarId { get; set; }
        public Cars Car {  get; set; }
    }
}
