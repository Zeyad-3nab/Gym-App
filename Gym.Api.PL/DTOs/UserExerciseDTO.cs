using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class UserExerciseDTO
    {
        [Required(ErrorMessage = "ApplicationUserIdRequired")]
        public string applicationUserId { get; set; }

        [Required(ErrorMessage ="ExerciseIdRequired")]
        public int exerciseId { get; set; }

        [Required(ErrorMessage = "NumOfGroupsRequired")]
        public int NumOfGroups { get; set; }

        [Required(ErrorMessage = "NumOfCountRequired")]
        public int NumOfCount { get; set; }
    }
}
