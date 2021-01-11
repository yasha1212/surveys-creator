using Microsoft.EntityFrameworkCore;
using System;
using ITechArt.DatabaseModels.Models;

namespace ITechArt.DatabaseModels
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
