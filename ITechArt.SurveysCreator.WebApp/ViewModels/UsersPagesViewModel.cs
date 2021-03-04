using System.Collections.Generic;
using ITechArt.SurveysCreator.Foundation.Models;

namespace ITechArt.SurveysCreator.WebApp.ViewModels
{
    public class UsersPagesViewModel
    {
        public IEnumerable<UserInfo> UsersInfo { get; set; }

        public PagesInfo PagesInfo { get; set; }
    }
}
