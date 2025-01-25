using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class FoodDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Number Of Calories is required")]
        public double NumOfCalories { get; set; }

        [Required(ErrorMessage = "Number Of Protein is required")]
        public double NumOfProtein { get; set; }

        [Required(ErrorMessage = "Image is required")]

        public IFormFile Image { get; set; }
    }
}
