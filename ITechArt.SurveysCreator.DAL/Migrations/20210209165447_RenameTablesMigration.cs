using Microsoft.EntityFrameworkCore.Migrations;

namespace ITechArt.SurveysCreator.DAL.Migrations
{
    public partial class RenameTablesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable("AspNetRoleClaims", "dbo", "RoleClaims", "dbo");
            migrationBuilder.RenameTable("AspNetRoles", "dbo", "Roles", "dbo");
            migrationBuilder.RenameTable("AspNetUserClaims", "dbo", "UserClaims", "dbo");
            migrationBuilder.RenameTable("AspNetUserLogins", "dbo", "UserLogins", "dbo");
            migrationBuilder.RenameTable("AspNetUserRoles", "dbo", "UserRoles", "dbo");
            migrationBuilder.RenameTable("AspNetUsers", "dbo", "Users", "dbo");
            migrationBuilder.RenameTable("AspNetUserTokens", "dbo", "UserTokens", "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable("RoleClaims", "dbo", "AspNetRoleClaims", "dbo");
            migrationBuilder.RenameTable("Roles", "dbo", "AspNetRoles", "dbo");
            migrationBuilder.RenameTable("UserClaims", "dbo", "AspNetUserClaims", "dbo");
            migrationBuilder.RenameTable("UserLogins", "dbo", "AspNetUserLogins", "dbo");
            migrationBuilder.RenameTable("UserRoles", "dbo", "AspNetUserRoles", "dbo");
            migrationBuilder.RenameTable("Users", "dbo", "AspNetUsers", "dbo");
            migrationBuilder.RenameTable("UserTokens", "dbo", "AspNetUserTokens", "dbo");
        }
    }
}
