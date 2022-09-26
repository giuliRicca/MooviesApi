using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoviesApi.Entities.Configurations
{
    public class AuditoriumConfig : IEntityTypeConfiguration<Auditorium>
    {
        public void Configure(EntityTypeBuilder<Auditorium> builder)
        {
            builder.Property(prop => prop.AuditoriumType)
                .HasDefaultValue(AuditoriumType.twoDimensions);

            builder.Property(prop => prop.Price)
                .HasPrecision(precision: 9, scale: 2);
        }
    }
}
