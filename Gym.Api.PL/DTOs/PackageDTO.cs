using Gym.Api.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class PackageDTO
    {
        [Required(ErrorMessage = "IdRequired")]
        public int Id { get; set; }


        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }

        [Required(ErrorMessage = "OldPriceRequired")]
        public double OldPrice { get; set; }

        [Required(ErrorMessage = "NewPriceRequired")]
        public double NewPrice { get; set; }

        [Required(ErrorMessage = "DurationRequired")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "IsActiveRequired")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "TypeRequired")]
        public string PackageType { get; set; }
    }
}
