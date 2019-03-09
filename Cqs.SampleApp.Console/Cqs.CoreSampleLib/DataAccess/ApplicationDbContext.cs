using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Cqs.CoreSampleLib.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Cqs.CoreSampleLib.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Database=cqs_books_test; Trusted_Connection=True;MultipleActiveResultSets=true";// Configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book[]
                {
                    new Book(){Id = 1,Authors = "J.R.Tolkien",DatePublished = DateTime.UtcNow.Subtract(TimeSpan.FromDays(3000)),InMyPossession = true,Title = "Lord of the Rings"},
                    new Book(){Id=2,Authors = "Blake Harris",DatePublished = DateTime.UtcNow.Subtract(TimeSpan.FromDays(3000)),InMyPossession = true,Title = "The History of the Future"},

                }
            );
        }
        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
