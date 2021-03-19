using ITechArt.SurveysCreator.DAL.Models.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITechArt.SurveysCreator.DAL.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasOne(q => q.Page)
                .WithMany(p => p.Questions)
                .HasForeignKey(q => q.PageId);
        }
    }
}
