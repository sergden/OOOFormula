using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using OOOFormula.Models;

namespace OOOFormula.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       
        public DbSet<Atributes> Atributes { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<Manufacturers> Manufacturers { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<OurServices> OurServices { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
