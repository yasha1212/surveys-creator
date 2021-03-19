using System.Collections.Generic;
using ITechArt.SurveysCreator.DAL.Models.Surveys;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.SurveysCreator.DAL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }

        public ICollection<Survey> Surveys { get; set; }
    }
}
