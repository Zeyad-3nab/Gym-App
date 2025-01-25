using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class UpdateuserDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Long is required")]
        public double Long { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public double Age { get; set; }

        [Required(ErrorMessage = "StartPackage is required")]
        public DateTime StartPackage { get; set; }

        [Required(ErrorMessage = "EndPackage is required")]
        public DateTime EndPackage { get; set; }

        [Required(ErrorMessage = "PackageId is required")]
        public int PackageId { get; set; }
    }
}
