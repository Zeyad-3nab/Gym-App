using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class FoodSystemDTO
    {

        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100 , ErrorMessage ="Maximum Length is 100")]
        public string Name { get; set; }

        [Required(ErrorMessage = "NumberOfCalories is required")]
        public double SumOfCalories { get; set; }

        [Required(ErrorMessage = "SumOfProteins is required")]
        public double SumOfProteins { get; set; }
    }
}
