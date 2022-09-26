using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata.Ecma335;

namespace MoviesApi.Entities.Configurations
{
    public class MovieConfig : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(prop => prop.Title)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(prop => prop.PosterUrl)
                .IsUnicode(false)
                .HasMaxLength(500);
        }
    }
}
