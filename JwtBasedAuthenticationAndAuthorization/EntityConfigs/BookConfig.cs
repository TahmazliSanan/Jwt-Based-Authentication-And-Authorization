using JwtBasedAuthenticationAndAuthorization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtBasedAuthenticationAndAuthorization.EntityConfigs
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("tbl_books");
            builder.HasKey(b => b.Id);
            builder.HasIndex(b => b.Name);
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(b => b.Price).IsRequired();
            builder.Property(b => b.PublishedDate).IsRequired(false);
        }
    }
}
