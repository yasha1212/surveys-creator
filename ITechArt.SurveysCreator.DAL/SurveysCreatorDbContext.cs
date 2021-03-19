using ITechArt.SurveysCreator.DAL.Configurations;
using Microsoft.EntityFrameworkCore;
using ITechArt.SurveysCreator.DAL.Models;
using ITechArt.SurveysCreator.DAL.Models.Surveys;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ITechArt.SurveysCreator.DAL
{
    public class SurveysCreatorDbContext : IdentityDbContext<User>
    {
        public DbSet<Survey> Surveys { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Question> Questions { get; set; }

        public SurveysCreatorDbContext(DbContextOptions<SurveysCreatorDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureTablesNaming();

            builder.SeedRolesAndAdmin();

            builder.ApplyConfiguration(new SurveyConfiguration());
            builder.ApplyConfiguration(new PageConfiguration());
            builder.ApplyConfiguration(new QuestionConfiguration());
        }
    }
}
