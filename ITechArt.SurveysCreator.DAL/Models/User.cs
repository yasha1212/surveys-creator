using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.SurveysCreator.DAL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
    }
}
