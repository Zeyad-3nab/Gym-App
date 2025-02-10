using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class UserFoodDTO
    {
        [Required(ErrorMessage = "ApplicationUserIdRequired")]
        public string applicationUserId { get; set; }

        [Required(ErrorMessage = "FoodIdRequired")]
        public int foodId { get; set; }

        [Required(ErrorMessage = "NumOfGramsRequired")]
        public int NumOfGrams { get; set; }

    }
}
