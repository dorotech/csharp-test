namespace Bookstore.Infrastructure.Data.Configurations.v1;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(128);

        builder.Property(x => x.Role);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(128);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(128);
    }
}