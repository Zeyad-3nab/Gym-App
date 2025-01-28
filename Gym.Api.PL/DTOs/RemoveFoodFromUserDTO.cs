using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class RemoveFoodFromUserDTO
    {
        [Required(ErrorMessage = "Food Id is required")]
        public int FoodId { get; set; }

        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; }
    }
}
