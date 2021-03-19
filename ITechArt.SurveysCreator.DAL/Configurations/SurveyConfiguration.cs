using ITechArt.SurveysCreator.DAL.Models.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITechArt.SurveysCreator.DAL.Configurations
{
    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.HasOne(s => s.Author)
                .WithMany(a => a.Surveys)
                .HasForeignKey(s => s.AuthorId);
        }
    }
}
