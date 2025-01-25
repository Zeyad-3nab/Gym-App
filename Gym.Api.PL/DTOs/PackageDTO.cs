using Gym.Api.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class PackageDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string PackageType { get; set; }
    }
}
