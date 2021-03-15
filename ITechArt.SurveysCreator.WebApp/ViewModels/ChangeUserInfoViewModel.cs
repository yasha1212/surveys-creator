using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITechArt.SurveysCreator.WebApp.ViewModels
{
    public class ChangeUserInfoViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Current email is not valid!")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        public List<string> AllRoles { get; set; }

        public ChangeUserInfoViewModel()
        {
            AllRoles = new List<string>();
        }
    }
}
