using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class UserReturnDTO
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public double Long { get; set; }
        public double Weight { get; set; }
        public double Age { get; set; }

        public string? ImageURL { get; set; }
        public string Gender { get; set; }

        public DateTime StartPackage { get; set; }

        public DateTime EndPackage { get; set; }
        public double ReminderOfPackage { get; set; }
        public int PackageId { get; set; }
    }
}