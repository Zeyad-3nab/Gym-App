using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class TrainerDataDTO
    {
        [Required(ErrorMessage = "IdRequired")]
        public int Id { get; set; }

        [Required(ErrorMessage = "UserNameRequired")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "AgeRequired")]
        public int Age { get; set; }

        [Required(ErrorMessage = "AddressRequired")]
        public string Address { get; set; }

        [Required(ErrorMessage = "PhoneRequired")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "EmailRequired")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "PasswordShape")]
        public string Password { get; set; }

        [Required(ErrorMessage = "LongRequired")]
        public double Long { get; set; }

        [Required(ErrorMessage = "WeightRequired")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "DailyWorkRequired")]
        public string DailyWork { get; set; }

        [Required(ErrorMessage = "AreYouSomkerRequired")]
        public string AreYouSomker { get; set; }

        [Required(ErrorMessage = "AimOfJoinRequired")]
        public string AimOfJoin { get; set; }

        [Required(ErrorMessage = "AnyPainsRequired")]
        public string AnyPains { get; set; }

        [Required(ErrorMessage = "AllergyOfFoodRequired")]
        public string AllergyOfFood { get; set; }

        [Required(ErrorMessage = "FoodSystemRequired")]
        public string FoodSystem { get; set; }

        [Required(ErrorMessage = "NumberOfMealsRequired")]
        public int NumberOfMeals { get; set; }

        [Required(ErrorMessage = "LastExerciseRequired")]
        public string LastExercise { get; set; }

        [Required(ErrorMessage = "AnyInfectionRequired")]
        public string AnyInfection { get; set; }

        [Required(ErrorMessage = "AbilityOfSystemMoneyRequired")]
        public string AbilityOfSystemMoney { get; set; }

        [Required(ErrorMessage = "NumberOfDayesRequired")]
        public int NumberOfDayes { get; set; }

        private string gender;

        [Required(ErrorMessage = "GenderRequired")]
        public string Gender
        {
            get { return gender; }
            set { gender = value.ToLower(); }
        }


        [Required(ErrorMessage = "StartPackageRequired")]
        public DateTime StartPackage { get; set; }

        [Required(ErrorMessage = "EndPackageRequired")]
        public DateTime EndPackage { get; set; }

        [Required(ErrorMessage = "PackageIdRequired")]
        public int PackageId { get; set; }
    }
}
