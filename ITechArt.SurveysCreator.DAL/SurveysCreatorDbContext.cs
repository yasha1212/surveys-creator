using Microsoft.EntityFrameworkCore;
using System;
using ITechArt.SurveysCreator.DAL.Models;

namespace ITechArt.SurveysCreator.DAL
{
    public class SurveysCreatorDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SurveysCreatorDbContext(DbContextOptions<SurveysCreatorDbContext> options)
            : base(options)
        { }
    }
}
