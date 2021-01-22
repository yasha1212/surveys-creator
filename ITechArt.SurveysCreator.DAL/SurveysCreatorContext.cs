using Microsoft.EntityFrameworkCore;
using System;
using ITechArt.SurveysCreator.DAL.Models;

namespace ITechArt.SurveysCreator.DAL
{
    public class SurveysCreatorContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SurveysCreatorContext(DbContextOptions<SurveysCreatorContext> options)
            : base(options)
        { }
    }
}
