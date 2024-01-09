using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id);
        
        builder.Property(user => user.FirstName);
        builder.Property(user => user.LastName);
        builder.Property(user => user.Email);
        builder.Property(user => user.Hash);
        builder.Property(user => user.Role).IsRequired(false);
    }
}