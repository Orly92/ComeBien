using ComeBien.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.DataAccess
{
    public class ComeBienContext : DbContext
    {
        public ComeBienContext() : base() { }

        public ComeBienContext(DbContextOptions<ComeBienContext> options)
            : base(options) { }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=comeBien;User Id=comeBien;Password=comeBien*;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(a => {
                a.HasKey(x => x.UserName);
            });

            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 1,
                EsName = "Pizza",
                EsDescription = "Construye tu propia pizza",
                EnName = "Pizza",
                EnDescription = "Build your own pizza",
                Price = 6
            }, new Products
            {
                Id = 2,
                EsName = "Hamburguesa",
                EsDescription = "Construye tu propia hamburguesa",
                EnName = "Burger",
                EnDescription = "Build your own burger",
                Price = 4
            });

            modelBuilder.Entity<Ingredients>().HasData(new Ingredients
            {
                Id = 1,
                EsName = "Queso",
                EnName = "Cheese",
                Price = 1
            }, new Ingredients
            {
                Id = 2,
                EsName = "Jamón",
                EnName = "Jam",
                Price = 1
            }, new Ingredients
            {
                Id = 3,
                EsName = "Bacon",
                EnName = "Bacon",
                Price = 1
            }, new Ingredients
            {
                Id = 4,
                EsName = "Chorizo",
                EnName = "Chorizo",
                Price = 1
            }, new Ingredients
            {
                Id = 5,
                EsName = "Piña",
                EnName = "Pineapple",
                Price = 1
            }, new Ingredients
            {
                Id = 6,
                EsName = "Cebolla",
                EnName = "Onion",
                Price = 1
            });

            modelBuilder.Entity<Administrator>()
                .HasData(new Administrator
                {
                    UserName = "admin",
                    Password = "admin1234*"
                });
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
    }
}
