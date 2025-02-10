using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class LoginDTO
    {
        [MaxLength(100 , ErrorMessage = "MaxLength100")]
        [DataType(DataType.EmailAddress , ErrorMessage = "EmailAddressDataType")]
        [Required(ErrorMessage = "EmailRequired")]
        public string Email { get; set; }


        [DataType(DataType.Password , ErrorMessage = "PasswwordDataType")]
        [Required(ErrorMessage = "PasswordRequired")]
        public string Password { get; set; }
    }
}
