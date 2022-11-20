using System.ComponentModel.DataAnnotations;

namespace BombaRestAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,12}$", ErrorMessage = "Passord must be between 4-12 length and contains: UpperCase,LowerCase,and digit,")]
        public string Password { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Length must be at least 3")]
        public string UserName { get; set; }
    }
}
