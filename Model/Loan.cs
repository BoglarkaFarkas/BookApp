using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace myappdotnet.Model;

public class Loan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int BookId { get; set; }

    [Required]
    public string Email { get; set; }

    public DateTime DateOfBorowing { get; set; }

    public DateTime DeadLine { get; set; }
    public bool IsFree { get; set; }

    [ForeignKey("BookId")]
    public Book Book { get; set; }
}

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.DateOfBorowing)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(u => u.DeadLine)
            .HasDefaultValueSql("CURRENT_TIMESTAMP + INTERVAL '90 days'")
            .IsRequired();

        builder.HasOne(b => b.Book)
            .WithMany()
            .HasForeignKey(b => b.BookId);

        builder.Property(u => u.IsFree)
            .IsRequired()
            .HasDefaultValue(false);

    }
}
