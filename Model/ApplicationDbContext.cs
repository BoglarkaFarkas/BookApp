using Microsoft.EntityFrameworkCore;

namespace myappdotnet.Model
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<Loan> Loan { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Author>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<BookAuthor>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Loan>()
            .Property(l => l.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.ApplyConfiguration(new LoanConfiguration());
        }
    }
}