using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ApplicationUser user = new ApplicationUser()
            {
                //we can use other types for id(IdentityUser<Guid>)
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "Admin*123");


            builder.Entity<ApplicationUser>().HasData(user);

            builder.Entity<ProductGroup>(productgroup =>
            {
                productgroup.HasIndex(x => x.GroupCode).IsUnique(true);
            });


            builder.Entity<Agreement>()
                   .HasOne(b => b.Product)
                   .WithMany(a => a.Agreements)
                   .HasForeignKey(f => f.ProductId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProductGroup>()
                .HasIndex(x => x.GroupCode)
                .IsUnique();


            builder.Entity<Product>()
                .HasIndex(x => x.ProductNumber)
                .IsUnique();



            builder.Entity<ProductGroup>().HasData(
                 new ProductGroup
                 {
                     Id = 1,
                     Active = true,
                     GroupCode = 2,
                     GroupDescription = "test1",
                 },
                 new ProductGroup
                 {
                     Id = 2,
                     Active = true,
                     GroupCode = 3,
                     GroupDescription = "test3",
                 },
                 new ProductGroup
                 {
                     Id = 3,
                     Active = false,
                     GroupCode = 4,
                     GroupDescription = "test3",
                 });

            builder.Entity<Product>().HasData(
               new Product
               {
                   Id = 1,
                   Active = true,
                   Price = 300,
                   ProductGroupID = 2,
                   ProductNumber = 12,
                   ProductDescription = "TestProductDescription",
               });

            builder.Entity<Agreement>().HasData(
               new Agreement
               {
                   Id = 1,
                   EffectiveDate = DateTime.Now,
                   ExpirationDate = DateTime.Now.AddDays(2),
                   NewPrice = 200,
                   ProductPrice = 300,
                   ProductGroupId = 1,
                   ProductId = 1,
                   UserId = user.Id,

               }, new Agreement
               {
                   Id = 2,
                   EffectiveDate = DateTime.Now,
                   ExpirationDate = DateTime.Now.AddDays(2),
                   NewPrice = 200,
                   ProductPrice = 300,
                   ProductGroupId = 1,
                   ProductId = 1,
                   UserId = user.Id,
               });
        }
    }
}
