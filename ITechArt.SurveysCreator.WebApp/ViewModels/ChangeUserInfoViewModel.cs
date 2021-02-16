﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ITechArt.SurveysCreator.WebApp.ViewModels
{
    public class ChangeUserInfoViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Current email is not valid!")]
        [Remote("IsUnique", "Account", HttpMethod = "POST", ErrorMessage = "Some user with this email is already signed up!")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
