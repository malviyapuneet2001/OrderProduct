using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entity.Repository;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace dal.Repository
{
    public class MyProjDb : DbContext
    {
        public MyProjDb()
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItem");
            modelBuilder.Entity<Product>().ToTable("Product");
        }

        protected override void  OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("Database=OrderDatabase;User ID= postgres;Password=1234;Server=localhost;Port=5432; Integrated Security =true;Pooling=true;");

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set;}
        public DbSet<OrderItem> Items { get; set;}
    }
}