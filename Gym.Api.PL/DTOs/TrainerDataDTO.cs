using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class TrainerDataDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, include an uppercase letter, a lowercase letter, a number, and a special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Long is required")]
        public double Long { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "DailyWork is required")]
        public string DailyWork { get; set; }

        [Required(ErrorMessage = "AreYouSomker is required")]
        public string AreYouSomker { get; set; }

        [Required(ErrorMessage = "AimOfJoin is required")]
        public string AimOfJoin { get; set; }

        [Required(ErrorMessage = "AnyPains is required")]
        public string AnyPains { get; set; }

        [Required(ErrorMessage = "AllergyOfFood is required")]
        public string AllergyOfFood { get; set; }

        [Required(ErrorMessage = "FoodSystem is required")]
        public string FoodSystem { get; set; }

        [Required(ErrorMessage = "NumberOfMeals is required")]
        public int NumberOfMeals { get; set; }

        [Required(ErrorMessage = "LastExercise is required")]
        public string LastExercise { get; set; }

        [Required(ErrorMessage = "AnyInfection is required")]
        public string AnyInfection { get; set; }

        [Required(ErrorMessage = "AbilityOfSystemMoney is required")]
        public string AbilityOfSystemMoney { get; set; }

        [Required(ErrorMessage = "NumberOfDayes is required")]
        public int NumberOfDayes { get; set; }

        [Required(ErrorMessage = "StartPackage is required")]
        public DateTime StartPackage { get; set; }

        [Required(ErrorMessage = "EndPackage is required")]
        public DateTime EndPackage { get; set; }

        [Required(ErrorMessage = "PackageId is required")]
        public int PackageId { get; set; }
    }
}
