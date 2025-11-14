namespace car_dealership_ASP.NET_Core_MVC.Models.Entities
{
    public class Images
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; } = string.Empty;

        // For deleting from Cloudinary
        public string PublicId { get; set; }

        public int CarId { get; set; }
        public Cars Car {  get; set; }
    }
}
