using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.SurveysCreator.DAL.Models;
using ITechArt.SurveysCreator.Foundation.Models;
using Microsoft.AspNetCore.Identity;

namespace ITechArt.SurveysCreator.Foundation.Services
{
    public interface IUserService
    { 
        Task<IdentityResult> SignUpAsync(User user, string password);

        Task<SignInResult> SignInAsync(string email, string password);

        Task SignOutAsync();

        bool ContainsByEmail(string email);

        bool ContainsById(string id);

        IEnumerable<UserInfo> GetUsersInfo(PagesInfo pagesInfo);

        int GetUserPagesCount(int pageSize);

        UserInfo GetUserInfo(string id);

        IEnumerable<string> GetRoles();

        Task<IdentityResult> EditAsync(UserInfo userInfo, string password);

        Task DeleteAsync(string id);

        Task<IdentityResult> CreateAsync(User user, string password, string role);
    }
}
