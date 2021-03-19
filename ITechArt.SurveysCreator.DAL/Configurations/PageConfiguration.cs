using ITechArt.SurveysCreator.DAL.Models.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITechArt.SurveysCreator.DAL.Configurations
{
    public class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasOne(p => p.Survey)
                .WithMany(s => s.Pages)
                .HasForeignKey(p => p.SurveyId);
        }
    }
}
