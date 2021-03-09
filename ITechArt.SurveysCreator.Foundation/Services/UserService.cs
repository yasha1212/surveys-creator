using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.SurveysCreator.DAL;
using ITechArt.SurveysCreator.DAL.Models;
using ITechArt.SurveysCreator.Foundation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.SurveysCreator.Foundation.Services
{
    public class UserService : IUserService
    {
        private readonly SurveysCreatorDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(SurveysCreatorDbContext context, UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> SignUpAsync(User user, string password)
        {
            return await CreateAsync(user, password, "user");
        }

        public async Task<SignInResult> SignInAsync(string email, string password)
        {
            return await _signInManager.PasswordSignInAsync(email, password, false, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> ContainsByEmailAsync(string email)
        {
            return await _context.Users
                .Select(u => u.NormalizedEmail)
                .ContainsAsync(email.ToUpper());
        }

        public async Task<bool> ContainsByIdAsync(string id)
        {
            return await _context.Users
                .Select(u => u.Id)
                .ContainsAsync(id);
        }

        public async Task<IEnumerable<UserInfo>> GetUsersInfoAsync(PagesInfo pagesInfo)
        {
            return await _context.Users
                .SelectMany(u => _context.UserRoles.Where(ur => ur.UserId == u.Id),
                    (u, ur) => new
                    {
                        u.Id,
                        u.Email,
                        u.FirstName,
                        u.SecondName,
                        ur.RoleId
                    })
                .SelectMany(x => _context.Roles.Where(r => r.Id == x.RoleId),
                    (x, r) => new UserInfo
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        SecondName = x.SecondName,
                        Role = r.Name
                    })
                .Skip((pagesInfo.PageNumber - 1) * pagesInfo.PageSize)
                .Take(pagesInfo.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetUserPagesCountAsync(int pageSize)
        {
            var usersCount = await _context.Users
                .SelectMany(u => _context.UserRoles.Where(ur => ur.UserId == u.Id),
                    (u, ur) => new
                    {
                        u.Id,
                        u.Email,
                        u.FirstName,
                        u.SecondName,
                        ur.RoleId
                    })
                .SelectMany(x => _context.Roles.Where(r => r.Id == x.RoleId),
                    (x, r) => new UserInfo
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        SecondName = x.SecondName,
                        Role = r.Name
                    })
                .CountAsync();

            var result = (double)usersCount / (double)pageSize;

            return (int)Math.Ceiling(result);
        }

        public async Task<UserInfo> GetUserInfoAsync(string id)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .SelectMany(u => _context.UserRoles.Where(ur => ur.UserId == u.Id).DefaultIfEmpty(),
                    (u, ur) => new
                    {
                        u.Id,
                        u.Email,
                        u.FirstName,
                        u.SecondName,
                        ur.RoleId
                    })
                .SelectMany(x => _context.Roles.Where(r => r.Id == x.RoleId).DefaultIfEmpty(),
                    (x, r) => new UserInfo
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        SecondName = x.SecondName,
                        Role = r.Name
                    })
                .FirstOrDefaultAsync();
        }

        public async Task<List<string>> GetRolesAsync()
        {
            return await _context.Roles
                .Select(r => r.Name)
                .ToListAsync();
        }

        public async Task<IdentityResult> EditAsync(UserInfo userInfo, string password)
        {
            var user = await _context.Users.FindAsync(userInfo.Id);

            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRoleAsync(user, userInfo.Role);

            user.Email = userInfo.Email;
            user.FirstName = userInfo.FirstName;
            user.SecondName = userInfo.SecondName;

            if (password != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
            }

            return await _userManager.UpdateAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);

            await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            return result;
        }
    }
}
