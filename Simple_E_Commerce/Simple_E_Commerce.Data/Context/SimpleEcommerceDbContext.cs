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
            #region Seed Data
            //Seed Data For Category
            modelBuilder.Entity<Category>().HasData
                (
                new Category()
                {
                    Id = 1,
                    CategoryName = "Shoes",
                    Description = "Running or Heeling Shoes"
                },
                new Category()
                {
                    Id = 2,
                    CategoryName = "Jeans",
                    Description = "Women Or Men Jeans"
                },
                 new Category()
                 {
                     Id = 3,
                     CategoryName = "T-Shirt",
                     Description = "Women And Men Round T-Shirt"
                 }
                );

            // Seed Data For Item
            modelBuilder.Entity<Item>().HasData(
                new Item()
                {
                    Id = 1,
                    Price = 19.0M,
                    QuantityInStock = 8
                },
            new Item()
            {
                Id = 2,
                Price = 16.0M,
                QuantityInStock = 12
            },
            new Item()
            {
                Id = 3,
                Price = 18.0M,
                QuantityInStock = 14
            });

            // Seed Data For Products
            modelBuilder.Entity<Product>().HasData(new Product()
            {
                Id = 1,
                ItemId = 1,
                ProductName = "RUNNING SHOES",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book"
            },
                new Product()
                {
                    Id = 2,
                    ItemId = 2,
                    ProductName = "WOMEN'S ROUND NECK T-SHIRT",
                    Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable."
                },
                new Product()
                {
                    Id = 3,
                    ItemId = 3,
                    ProductName = "WOMEN'S JEANS",
                    Description = "If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet"
                });

            // Seed Data For CategoryToProduct
            modelBuilder.Entity<CategoryToProduct>().HasData(
                new CategoryToProduct() { CategoryId = 1, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 1, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 1, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 3 }
                );
            #endregion

            modelBuilder.Entity<CategoryToProduct>().HasKey(t => new { t.ProductId, t.CategoryId });

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
