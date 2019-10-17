using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HelloWorld.Core.Domain.Entities;
using HelloWorld.Core.Infrastructure.Data.Mapping;
using System.IO;

namespace HelloWorld.Core.Infrastructure.Data.Context
{
    public class SerkoCoreDataContext : DbContext
    {
       
        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Expense> Expense { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyConfiguration(new ExpenseMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           /* var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            */
        }
       
    }
}