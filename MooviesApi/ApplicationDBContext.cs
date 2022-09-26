using EFCorePeliculas.Entidades.Seeding;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Entities;
using System.Reflection;

namespace MoviesApi
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<DateTime>().HaveColumnType("Date");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            SeedingModuloConsulta.Seed(modelBuilder);

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies{ get; set; }
        public DbSet<CinemaOffer> CinemaOffers { get; set; }
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<MovieActor> MoviesActors{ get; set; }
    }
}
