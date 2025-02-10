using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class FoodDTO
    {
        [Required(ErrorMessage = "IdRequired")]
        public int Id { get; set; }
        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }
        [Required(ErrorMessage = "NumOfCaloriesRequired")]
        public double NumOfCalories { get; set; }

        [Required(ErrorMessage = "NumOfProteinRequired")]
        public double NumOfProtein { get; set; }
        public string? ImageURL { get; set; }

        [Required(ErrorMessage = "ImageRequired")]
        public IFormFile Image { get; set; }
    }
}