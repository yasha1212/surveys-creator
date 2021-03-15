using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ITechArt.SurveysCreator.WebApp.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Current email is not valid!")]
        [Remote("IsUnique", "Account", HttpMethod = "POST", ErrorMessage = "Some user with this email is already signed up!")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }

        [Required]
        public string Role { get; set; }

        public List<string> AllRoles { get; set; }

        public CreateUserViewModel()
        {
            AllRoles = new List<string>();
        }
    }
}
