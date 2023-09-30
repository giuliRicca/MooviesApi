using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoviesApi.Entities.Configurations
{
    public class CinemaMovieConfig : IEntityTypeConfiguration<CinemaMovie>
    {
        public void Configure(EntityTypeBuilder<CinemaMovie> builder)
        {
            builder.HasKey(prop =>
            new { prop.CinemaId, prop.MovieId });

        }
    }
}
