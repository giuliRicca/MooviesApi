using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoviesApi.Entities.Configurations
{
    public class GenreConfig : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(prop => prop.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(g => g.Name).IsUnique();
        }
    }
}
