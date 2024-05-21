using LibraryAPI.Constants;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LibraryAPI.Data
{
    public class DataContext : IdentityDbContext
    {

        public DataContext(DbContextOptions<DataContext> options):base(options) 
        {            
        }

        public DbSet<Author> Authors { get; set; }  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var adminUser = new IdentityUser
            {
                Id = "userId",
                UserName = "admin@valcon.com",
                NormalizedUserName = "ADMIN@VALCON.COM",
                Email = "admin@valcon.com",
                NormalizedEmail = "ADMIN@VALCON.COM",
                EmailConfirmed = true
            };
            var passwordHasher = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "passworD1!");
            
            modelBuilder.Entity<IdentityUser>().HasData(adminUser);
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole<string> { Id = LibraryRoleIds.Admin, Name = LibraryRoles.Admin },
                new IdentityRole<string> { Id = LibraryRoleIds.Librarian, Name = LibraryRoles.Librarian },
                new IdentityRole<string> { Id = LibraryRoleIds.User, Name = LibraryRoles.User });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = LibraryRoleIds.Admin,
                UserId = adminUser.Id
            });
            
        }
    }
}
