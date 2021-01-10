using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveysCreator.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
