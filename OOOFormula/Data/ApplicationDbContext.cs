using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;

namespace OOOFormula.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Debug message to console
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message)); //Messages to Degub console
        //    //  optionsBuilder.LogTo(System.Console.WriteLine); 
        //    optionsBuilder.LogTo(System.Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }); //Message to console app
        //}

        public DbSet<Category> Category { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<Manufacturers> Manufacturers { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Requests> Requests { get; set; }
    }
}
