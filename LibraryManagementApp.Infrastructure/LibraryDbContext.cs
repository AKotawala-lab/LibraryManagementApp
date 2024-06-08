// LibraryDbContext.cs
using LibraryManagementApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Infrastructure.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure entity properties and relationships if needed

             // Seed authors
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "Shakespeare", Description = "Shakespeare is a great author." },
                new Author { AuthorId = 2, Name = "Robert Kiyosaki", Description = "Robert Kiyosaki is a famous author." }
            );

            // Seed books
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Romeo & Juliet", Description = "Evergreen love story", AuthorId = 1 },
                new Book { BookId = 2, Title = "Hamlet", Description = "Description 2", AuthorId = 1 }
            );
        }
    }
}
