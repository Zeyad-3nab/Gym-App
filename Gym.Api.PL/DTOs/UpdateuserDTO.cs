using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class UpdateuserDTO
    {
        [Required(ErrorMessage = "IdRequired")]
        public string Id { get; set; }

        [Required(ErrorMessage = "UserNameRequired")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PhoneRequired")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "EmailRequired")]
        public string Email { get; set; }

        [Required(ErrorMessage = "AddressRequired")]
        public string Address { get; set; }

        [Required(ErrorMessage = "LongRequired")]
        public double Long { get; set; }

        [Required(ErrorMessage = "WeightRequired")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "AgeRequired")]
        public double Age { get; set; }

        [Required(ErrorMessage = "StartPackageRequired")]
        public DateTime StartPackage { get; set; }

        [Required(ErrorMessage = "EndPackageRequired")]
        public DateTime EndPackage { get; set; }

        [Required(ErrorMessage = "PackageIdRequired")]
        public int PackageId { get; set; }
    }
}
