using Microsoft.EntityFrameworkCore;
using ITechArt.SurveysCreator.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ITechArt.SurveysCreator.DAL
{
    public class SurveysCreatorDbContext : IdentityDbContext<User>
    {
        public SurveysCreatorDbContext(DbContextOptions<SurveysCreatorDbContext> options)
            : base(options)
        { }
    }
}
