using JwtBasedAuthenticationAndAuthorization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace JwtBasedAuthenticationAndAuthorization.EntityConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tbl_users");
            builder.HasKey(b => b.Id);
            builder.HasIndex(b => new { b.Id, b.FirstName, b.LastName });

            builder.HasData(new User
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin.company-name@gmail.com",
                Password = "admin123456789",
                BirthDateTime = DateTime.SpecifyKind(new DateTime(2024, 1, 1), DateTimeKind.Utc),
                Roles = new List<string> { "Admin" },
                CreatedDateTime = DateTime.UtcNow
            });

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

            builder.Property(b => b.Roles)
                .HasConversion(
                    roles => JsonConvert.SerializeObject(roles),
                    roles => JsonConvert.DeserializeObject<List<string>>(roles) ?? new List<string>());
        }
    }
}
