using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class UserExerciseDTO
    {
        [Required(ErrorMessage ="UserId is required")]
        public string applicationUserId { get; set; }

        [Required(ErrorMessage ="ExerciseId is required")]
        public int exerciseId { get; set; }

        [Required(ErrorMessage ="Number Of Groups is required")]
        public int NumOfGroups { get; set; }

        [Required(ErrorMessage ="Number Of Count is required")]
        public int NumOfCount { get; set; }
    }
}
