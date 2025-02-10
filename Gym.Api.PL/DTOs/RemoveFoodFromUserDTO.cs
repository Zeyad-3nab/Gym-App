using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class RemoveFoodFromUserDTO
    {
        [Required(ErrorMessage = "FoodIdRequired")]
        public int FoodId { get; set; }

        [Required(ErrorMessage = "UserIdRequired")]
        public string UserId { get; set; }
    }
}
