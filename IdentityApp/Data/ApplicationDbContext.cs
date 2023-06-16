using IdentityApp.Models;
using IdentityApp.Models.Repositories;
using IdentityEcommerce.Models;
using IdentityEcommerce.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityEcommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new Category { ID = 1, Name = "Computers"});
            modelBuilder.Entity<Category>().HasData(new Category { ID = 2, Name = "Games"});
            modelBuilder.Entity<Category>().HasData(new Category { ID = 3, Name = "Cellphones"});

        }
    }
}
