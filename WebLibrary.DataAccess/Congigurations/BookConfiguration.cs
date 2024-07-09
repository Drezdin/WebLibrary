using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLibrary.DataAccess.Entites;
using WebLibrary.Core.Models;

namespace WebLibrary.DataAccess.Congigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Title).IsRequired().HasMaxLength(Book.MaxLenght);
            builder.Property(b => b.Author).IsRequired().HasMaxLength(Book.MaxLenght);
            builder.Property(b => b.Genre).IsRequired().HasMaxLength(Book.MaxLenght);
            builder.Property(b => b.Publisher).IsRequired().HasMaxLength(Book.MaxLenght);
            builder.Property(b => b.IsBooked).IsRequired();
        }
    }
}
