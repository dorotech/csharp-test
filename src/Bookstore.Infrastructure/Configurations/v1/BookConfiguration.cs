namespace Bookstore.Infrastructure.Data.Configurations.v1;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(256);

        builder.Property(x => x.Pages)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(128);

        builder.Property(x => x.Publisher)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(256);

        builder.Property(x => x.Year)
            .IsRequired();

        builder.Property(x => x.Author)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(256);

        builder.Property(x => x.Genre)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(256);
    }
}