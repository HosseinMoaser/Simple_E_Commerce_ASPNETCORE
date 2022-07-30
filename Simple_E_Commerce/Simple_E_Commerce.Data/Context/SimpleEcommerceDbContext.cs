using Microsoft.EntityFrameworkCore;
using Simple_E_Commerce.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_E_Commerce.Data.Context
{
    public class SimpleEcommerceDbContext : DbContext
    {
        public SimpleEcommerceDbContext(DbContextOptions<SimpleEcommerceDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<CategoryToProduct> CategoryToProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Category Seed Data
            modelBuilder.Entity<Category>().HasData
                (
                new Category()
                {
                    Id = 1,
                    CategoryName = "Shoes",
                    Description = "Various Types Of Shoes"
                },
                new Category()
                {
                    Id = 2,
                    CategoryName = "Watches",
                    Description = "For Women and Men"
                }
                );
            #endregion

            modelBuilder.Entity<CategoryToProduct>().HasKey(t=> new {t.ProductId , t.CtegoryId});

            // Sample Relation Using Fluent-API

            //modelBuilder.Entity<Product>(
            //    p =>
            //    {
            //        p.HasKey(pr => pr.Id);
            //        p.OwnsOne<Item>(pr => pr.Item);
            //        p.HasOne<Item>(pr => pr.Item).WithOne(pr=> pr.Product).HasForeignKey<Item>(it=> it.Id);
            //    }) ;

            // Sample Key Definition And ColumnType Definition Using Fluent-API

            //modelBuilder.Entity<Item>(
            //    it=>
            //    {
            //        it.HasKey(item => item.Id);
            //        it.Property(item => item.Price).HasColumnType("Money");
            //    });


            base.OnModelCreating(modelBuilder);
        }
    }
}
