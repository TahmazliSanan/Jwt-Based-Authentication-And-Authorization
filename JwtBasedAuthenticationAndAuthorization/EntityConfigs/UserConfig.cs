using JwtBasedAuthenticationAndAuthorization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtBasedAuthenticationAndAuthorization.EntityConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tbl_users");
            builder.HasKey(b => b.Id);
            builder.HasIndex(b => new { b.Id, b.FirstName, b.LastName });

            builder.Property(b => b.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Password)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.BirthDateTime)
                .IsRequired(false);
        }
    }
}
