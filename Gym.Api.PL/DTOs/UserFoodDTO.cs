using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class UserFoodDTO
    {
        [Required(ErrorMessage = "UserId is required")]
        public string applicationUserId { get; set; }

        [Required(ErrorMessage = "FoodId is required")]
        public int foodId { get; set; }

        [Required(ErrorMessage = "Number Of Grams is required")]
        public int NumOfGrams { get; set; }

    }
}
