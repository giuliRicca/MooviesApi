using Microsoft.EntityFrameworkCore;
using MoviesApi.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCorePeliculas.Entidades.Seeding
{
    public static class SeedingModuloConsulta
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var acción = new Genre { Id = 1, Name = "Acción" };
            var animación = new Genre { Id = 2, Name = "Animación" };
            var comedia = new Genre { Id = 3, Name = "Comedia" };
            var cienciaFicción = new Genre { Id = 4, Name = "Ciencia ficción" };
            var drama = new Genre { Id = 5, Name = "Drama" };

            modelBuilder.Entity<Genre>().HasData(acción, animación, comedia, cienciaFicción, drama);

            var tomHolland = new Actor() { Id = 1, Name = "Tom Holland", Birthday = new DateTime(1996, 6, 1), Biography = "Thomas Stanley Holland (Kingston upon Thames, Londres; 1 de junio de 1996), conocido simplemente como Tom Holland, es un actor, actor de voz y bailarín británico." };
            var samuelJackson = new Actor() { Id = 2, Name = "Samuel L. Jackson", Birthday = new DateTime(1948, 12, 21), Biography = "Samuel Leroy Jackson (Washington D. C., 21 de diciembre de 1948), conocido como Samuel L. Jackson, es un actor y productor de cine, televisión y teatro estadounidense. Ha sido candidato al premio Óscar, a los Globos de Oro y al Premio del Sindicato de Actores, así como ganador de un BAFTA al mejor actor de reparto." };
            var robertDowney = new Actor() { Id = 3, Name = "Robert Downey Jr.", Birthday = new DateTime(1965, 4, 4), Biography = "Robert John Downey Jr. (Nueva York, 4 de abril de 1965) es un actor, actor de voz, productor y cantante estadounidense. Inició su carrera como actor a temprana edad apareciendo en varios filmes dirigidos por su padre, Robert Downey Sr., y en su infancia estudió actuación en varias academias de Nueva York." };
            var chrisEvans = new Actor() { Id = 4, Name = "Chris Evans", Birthday = new DateTime(1981, 06, 13) };
            var laRoca = new Actor() { Id = 5, Name = "Dwayne Johnson", Birthday = new DateTime(1972, 5, 2) };
            var auliCravalho = new Actor() { Id = 6, Name = "Auli'i Cravalho", Birthday = new DateTime(2000, 11, 22) };
            var scarlettJohansson = new Actor() { Id = 7, Name = "Scarlett Johansson", Birthday = new DateTime(1984, 11, 22) };
            var keanuReeves = new Actor() { Id = 8, Name = "Keanu Reeves", Birthday = new DateTime(1964, 9, 2) };

            modelBuilder.Entity<Actor>().HasData(tomHolland, samuelJackson,
                            robertDowney, chrisEvans, laRoca, auliCravalho, scarlettJohansson, keanuReeves);
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            //var agora = new Cinema() { Id = 1, Name = "Hoyts Nuevocentro", Location = geometryFactory.CreatePoint(new Coordinate(-64.205345, -31.412223)) };
            //var sambil = new Cinema() { Id = 2, Name = "Hoyts Patioolmos", Location = geometryFactory.CreatePoint(new Coordinate(-64.18821224609333, -31.41983153249215)) };
            //var megacentro = new Cinema() { Id = 3, Name = "Gran Rex", Location = geometryFactory.CreatePoint(new Coordinate(-64.18552592889834, -31.413265739499995)) };
            //var acropolis = new Cinema() { Id = 4, Name = "Dinosaurio Mall Ruta20", Location = geometryFactory.CreatePoint(new Coordinate(-64.21224090006244, -31.42828243550159)) };

            //var agoraCinemaOffer = new CinemaOffer { Id = 1, CinemaId = agora.Id, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), DiscountPercentage = 10 };

            //var acropolisCinemaOffer = new CinemaOffer { Id = 2, CinemaId = acropolis.Id, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(5), DiscountPercentage = 15 };

            //modelBuilder.Entity<Cinema>().HasData(acropolis, sambil, megacentro, agora);
            //modelBuilder.Entity<CinemaOffer>().HasData(acropolisCinemaOffer, agoraCinemaOffer);

            var avengers = new Movie()
            {
                Id = 1,
                Title = "Avengers",
                OnBillboard = false,
                PremiereDate = new DateTime(2012, 4, 11),
                PosterUrl = "https://upload.wikimedia.org/wikipedia/en/8/8a/The_Avengers_%282012_film%29_poster.jpg",
            };

            var MovieGenreEntity = "MovieGenre";
            var GenreIdProperty = "GenresId";
            var MovieIdProperty = "MoviesId";

            modelBuilder.Entity(MovieGenreEntity).HasData(
                new Dictionary<string, object> { [GenreIdProperty] = acción.Id, [MovieIdProperty] = avengers.Id },
                new Dictionary<string, object> { [GenreIdProperty] = cienciaFicción.Id, [MovieIdProperty] = avengers.Id }
            );


            var noWayHome = new Movie()
            {
                Id = 3,
                Title = "Spider-Man: No way home",
                OnBillboard = false,
                PremiereDate = new DateTime(2021, 12, 17),
                PosterUrl = "https://upload.wikimedia.org/wikipedia/en/0/00/Spider-Man_No_Way_Home_poster.jpg"
            };

            modelBuilder.Entity(MovieGenreEntity).HasData(
               new Dictionary<string, object> { [GenreIdProperty] = cienciaFicción.Id, [MovieIdProperty] = noWayHome.Id },
               new Dictionary<string, object> { [GenreIdProperty] = acción.Id, [MovieIdProperty] = noWayHome.Id },
               new Dictionary<string, object> { [GenreIdProperty] = comedia.Id, [MovieIdProperty] = noWayHome.Id }
           );

            var farFromHome = new Movie()
            {
                Id = 4,
                Title = "Spider-Man: Far From Home",
                OnBillboard = false,
                PremiereDate = new DateTime(2019, 7, 2),
                PosterUrl = "https://upload.wikimedia.org/wikipedia/en/0/00/Spider-Man_No_Way_Home_poster.jpg"
            };

            modelBuilder.Entity(MovieGenreEntity).HasData(
               new Dictionary<string, object> { [GenreIdProperty] = cienciaFicción.Id, [MovieIdProperty] = farFromHome.Id },
               new Dictionary<string, object> { [GenreIdProperty] = acción.Id, [MovieIdProperty] = farFromHome.Id },
               new Dictionary<string, object> { [GenreIdProperty] = comedia.Id, [MovieIdProperty] = farFromHome.Id }
           );

            var theMatrixResurrections = new Movie()
            {
                Id = 5,
                Title = "The Matrix Resurrections",
                OnBillboard = true,
                PremiereDate = DateTime.Today,
                PosterUrl = "https://upload.wikimedia.org/wikipedia/en/5/50/The_Matrix_Resurrections.jpg",
            };

            modelBuilder.Entity(MovieGenreEntity).HasData(
              new Dictionary<string, object> { [GenreIdProperty] = cienciaFicción.Id, [MovieIdProperty] = theMatrixResurrections.Id },
              new Dictionary<string, object> { [GenreIdProperty] = acción.Id, [MovieIdProperty] = theMatrixResurrections.Id },
              new Dictionary<string, object> { [GenreIdProperty] = drama.Id, [MovieIdProperty] = theMatrixResurrections.Id }
          );



            var keanuReevesMatrix = new MovieActor
            {
                ActorId = keanuReeves.Id,
                MovieId = theMatrixResurrections.Id,
                Order = 1,
                Character = "Neo"
            };

            var avengersChrisEvans = new MovieActor
            {
                ActorId = chrisEvans.Id,
                MovieId = avengers.Id,
                Order = 1,
                Character = "Capitán América"
            };

            var avengersRobertDowney = new MovieActor
            {
                ActorId = robertDowney.Id,
                MovieId = avengers.Id,
                Order = 2,
                Character = "Iron Man"
            };

            var avengersScarlettJohansson = new MovieActor
            {
                ActorId = scarlettJohansson.Id,
                MovieId = avengers.Id,
                Order = 3,
                Character = "Black Widow"
            };

            var tomHollandFFH = new MovieActor
            {
                ActorId = tomHolland.Id,
                MovieId = farFromHome.Id,
                Order = 1,
                Character = "Peter Parker"
            };

            var tomHollandNWH = new MovieActor
            {
                ActorId = tomHolland.Id,
                MovieId = noWayHome.Id,
                Order = 1,
                Character = "Peter Parker"
            };

            var samuelJacksonFFH = new MovieActor
            {
                ActorId = samuelJackson.Id,
                MovieId = farFromHome.Id,
                Order = 2,
                Character = "Samuel L. Jackson"
            };

            modelBuilder.Entity<Movie>().HasData(avengers, noWayHome, farFromHome, theMatrixResurrections);
            modelBuilder.Entity<MovieActor>().HasData(samuelJacksonFFH, tomHollandFFH, tomHollandNWH, avengersRobertDowney, avengersScarlettJohansson,
                avengersChrisEvans, keanuReevesMatrix);

        }
    }
}