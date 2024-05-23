using LibraryAPI.Constants;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LibraryAPI.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookRent> BookRents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var adminUser = new User
            {
                Id = "userId",
                UserName = "admin@valcon.com",
                NormalizedUserName = "ADMIN@VALCON.COM",
                Email = "admin@valcon.com",
                NormalizedEmail = "ADMIN@VALCON.COM",
                EmailConfirmed = true
            };
            var passwordHasher = new PasswordHasher<User>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "passworD1!");

            modelBuilder.Entity<User>().HasData(adminUser);
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole<string> { Id = LibraryRoleIds.Admin, Name = LibraryRoles.Admin },
                new IdentityRole<string> { Id = LibraryRoleIds.Librarian, Name = LibraryRoles.Librarian },
                new IdentityRole<string> { Id = LibraryRoleIds.User, Name = LibraryRoles.User });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = LibraryRoleIds.Admin,
                UserId = adminUser.Id
            });

            modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithMany(b => b.Authors)
            .UsingEntity<Dictionary<string, object>>(
                 "AuthorBook",
            ab => ab.HasOne<Book>().WithMany().HasForeignKey("BookId"),
            ab => ab.HasOne<Author>().WithMany().HasForeignKey("AuthorId"));

            modelBuilder.Entity<BookRent>()
                .HasKey(r => new {r.Id });
            modelBuilder.Entity<BookRent>()
                .HasOne(r => r.User)
                .WithMany(u => u.BookRents)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<BookRent>()
                .HasOne(r => r.Book)
                .WithMany(b => b.BookRents)
                .HasForeignKey(r => r.BookId);
        }
    }    
}
