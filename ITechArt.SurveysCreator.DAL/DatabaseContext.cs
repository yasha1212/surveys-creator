using Microsoft.EntityFrameworkCore;
using System;
using ITechArt.SurveysCreator.DAL.Models;

namespace ITechArt.SurveysCreator.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
