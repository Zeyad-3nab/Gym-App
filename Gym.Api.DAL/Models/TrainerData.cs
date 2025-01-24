using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Models
{
    public class TrainerData:BaseModel<int>
    {
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Long { get; set; }
        public double Weight { get; set; }
        public string DailyWork { get; set; }
        public string AreYouSomker { get; set; }
        public string AimOfJoin { get; set; }
        public string AnyPains { get; set; }
        public string AllergyOfFood { get; set; }
        public string FoodSystem { get; set; }
        public int NumberOfMeals { get; set; }
        public string LastExercise { get; set; }
        public string AnyInfection { get; set; }
        public string AbilityOfSystemMoney { get; set; }
        public int NumberOfDayes { get; set; }

        public int PackageId { get; set; }
        public Package Package { get; set; }

    }
}
