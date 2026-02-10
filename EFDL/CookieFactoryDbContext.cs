using Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Linq_och_EF_Workshop
{
    public class CookieFactoryDbContext : DbContext
    {

        public DbSet<Cookie> Cookies { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CookieFactoryDB;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cookie>().Property(c => c.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Cookie>().HasIndex(c => c.Name).IsUnique();
        }


        public void FillWithTestData()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            var ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Mjöl", Price = 10 },
                new Ingredient { Name = "Smör", Price = 20 },
                new Ingredient { Name = "Socker", Price = 5 },
                new Ingredient { Name = "Kakao", Price = 12 },
                new Ingredient { Name = "Havregryn", Price = 6 }
            };

            Ingredients.AddRange(ingredients);
            SaveChanges();

            var flour = Ingredients.First(i => i.Name == "Mjöl");
            var butter = Ingredients.First(i => i.Name == "Smör");
            var sugar = Ingredients.First(i => i.Name == "Socker");
            var cocoa = Ingredients.First(i => i.Name == "Kakao");
            var oatFlakes = Ingredients.First(i => i.Name == "Havregryn");

            var cookies = new List<Cookie>
            {
                new Cookie { Name = "Chokladboll", Description = "En klassisk chokladboll", Price = 7,
                Ingredients= new List<Ingredient> {butter, sugar, cocoa, oatFlakes}
                },
                new Cookie { Name = "Kokosboll", Description = "En klassisk kokosboll", Price = 6,
                Ingredients= new List<Ingredient> {butter, sugar, cocoa, oatFlakes}
                },
                new Cookie { Name = "Havrekaka", Description = "En klassisk havrekaka", Price = 5,
                Ingredients= new List<Ingredient> {butter, sugar, flour, oatFlakes}                               },
            };

            Cookies.AddRange(cookies);
            SaveChanges();

        }
    }
    
}
