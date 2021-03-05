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

        //public DbSet<Atributes> Atributes { get; set; }
        //public DbSet<AtributesValues> AtributesValues { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<Manufacturers> Manufacturers { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Requests> Requests { get; set; }
    }
}
