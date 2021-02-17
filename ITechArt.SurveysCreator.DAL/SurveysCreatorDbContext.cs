using Microsoft.EntityFrameworkCore;
using ITechArt.SurveysCreator.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ITechArt.SurveysCreator.DAL
{
    public class SurveysCreatorDbContext : IdentityDbContext<User>
    {
        public SurveysCreatorDbContext(DbContextOptions<SurveysCreatorDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

            const string adminId = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string roleAdminId = "2a220226-4849-4997-9432-67238d70514e";
            const string roleUserId = "a3a88ce7-9959-4a6d-85f6-a538e6f33171";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = roleAdminId,
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                
                new IdentityRole
                {
                    Id = roleUserId,
                    Name = "user",
                    NormalizedName = "USER"
                });

            var adminUser = new User
            {
                Id = adminId,
                UserName = "admin@admin.com",
                NormalizedUserName = "admin@admin.com".ToUpper(),
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com".ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = string.Empty
            };

            var hasher = new PasswordHasher<User>();

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "admin-admin22");

            builder.Entity<User>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleAdminId,
                UserId = adminId
            });
        }
    }
}
