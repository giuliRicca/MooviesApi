using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoviesApi.Entities.Configurations
{
    public class CinemaConfig : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.Property(prop => prop.Name)
                .HasMaxLength(120)
                .IsRequired();

            builder.HasOne(c => c.CinemaOffer)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<CinemaOffer>(co => co.CinemaId);

            builder.HasMany(c => c.Auditoriums)
                .WithOne(a => a.Cinema)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(a => a.CinemaId);
        }
    }
}
