using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.SurveysCreator.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.SurveysCreator.Foundation.Services
{
    public interface IUserService
    { 
        Task<IdentityResult> SignUpAsync(User user, string password);

        Task<SignInResult> SignInAsync(string email, string password);

        Task SignOutAsync();

        bool Contains(string email);
    }
}
