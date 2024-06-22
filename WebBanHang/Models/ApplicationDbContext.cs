using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WebBanHang.Models
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
          
        }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed data to table Categories
            modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Điện thoại", DisplayOrder = 1 },
            new Category { Id = 2, Name = "Máy tính bảng", DisplayOrder = 2 },
            new Category { Id = 3, Name = "Laptop", DisplayOrder = 3 });
            //seed data to table Product
           
        }


    }
}
