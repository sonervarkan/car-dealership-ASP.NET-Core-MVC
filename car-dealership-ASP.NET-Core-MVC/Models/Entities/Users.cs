using System.ComponentModel.DataAnnotations;

namespace car_dealership_ASP.NET_Core_MVC.Models.Entities
{
    public class Users
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [DataType(DataType.Password)]
        [MinLength(2, ErrorMessage = "Şifre en az 2 karakter olmalıdır.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bir rol seçimi zorunludur.")]
        public int? RoleId { get; set; } // ? (nullable) olmazsa işlem yapmıyor
        public Roles? Role { get; set; } // ? (nullable) olmazsa işlem yapmıyor

        public ICollection<Cars> Cars { get; set; } = new List<Cars>();
    }
}
