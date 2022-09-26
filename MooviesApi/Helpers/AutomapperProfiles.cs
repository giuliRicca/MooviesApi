using AutoMapper;
using MoviesApi.DTOs;
using MoviesApi.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace MoviesApi.Helpers
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            // Cinema -> CinemaDTO
            CreateMap<Cinema, CinemaDTO>()
                .ForMember(dto => dto.Latitude, ent => ent.MapFrom(prop => prop.Location.Y))
                .ForMember(dto => dto.Longitude, ent => ent.MapFrom(prop => prop.Location.X));

            // CinemaCreationDTO -> Cinema
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            CreateMap<CinemaCreationDTO, Cinema>()
                .ForMember(ent => ent.CinemaOffer, dto => dto.MapFrom(prop=> new CinemaOffer()
                {
                    StartDate = prop.DiscountStartDate,
                    EndDate = prop.DiscountEndDate,
                    DiscountPercentage = prop.DiscountPercentage
                }))
                .ForMember(ent => ent.Location, dto => dto.MapFrom(prop =>
                    geometryFactory.CreatePoint(new Coordinate(prop.Longitude, prop.Latitude))));

            

            // AuditoriumCreationDTO -> Auditorium
            CreateMap<AuditoriumCreationDTO, Auditorium>();

            
            // Actor -> ActorDTO
            CreateMap<Actor, ActorDTO>().ReverseMap();
            // ActorCreationDTO -> Actor
            // Genre <-> GenreDTO
            CreateMap<Genre, GenreDTO>().ReverseMap();
            // GenreCreationDTO -> Genre
            CreateMap<GenreCreationDTO, Genre>();

            // Movie -> MovieDTO
            CreateMap<Movie, MovieDTO>()
                .ForMember(dto => dto.Genres, ent => ent.MapFrom(prop => 
                                                prop.Genres.OrderByDescending(g => g.Name)))
                .ForMember(dto => dto.Cinemas, ent => ent.MapFrom(prop => prop.Auditoriums.Select(a => a.Cinema)))
                .ForMember(dto => dto.Actors, ent => ent.MapFrom(prop => prop.MovieActors.Select(ma => ma.Actor)));
            // MovieCreationDTO -> Movie
            CreateMap<MovieCreationDTO, Movie>()
                .ForMember(ent => ent.Genres, dto => dto.MapFrom(prop =>
                        prop.GenresIds.Select(id => new Genre() { Id = id })))
                .ForMember(ent => ent.Auditoriums, dto => dto.MapFrom(prop =>
                        prop.AuditoriumsIds.Select(id => new Auditorium() { Id = id })))
                .ForMember(ent => ent.MovieActors, dto => dto.MapFrom(prop => prop.MovieActorCreationDTOs.Select(
                    ma => new MovieActor()
                    {
                        ActorId = ma.ActorId,
                        Character = ma.Character
                    })));


            // MovieActorCreationDTO -> MovieActor
            CreateMap<MovieActorCreationDTO, MovieActor>();

        }
    }
}
