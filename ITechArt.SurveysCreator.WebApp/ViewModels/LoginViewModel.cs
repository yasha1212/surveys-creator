using System.ComponentModel.DataAnnotations;

namespace ITechArt.SurveysCreator.WebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Current email is not valid!")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
