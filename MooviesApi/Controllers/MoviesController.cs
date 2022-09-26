using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MoviesApi.DTOs;
using MoviesApi.Entities;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/movie")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public MoviesController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movieDB = await context.Movies
                .Include(m => m.Genres)
                .Include(m => m.Auditoriums)
                    .ThenInclude(a => a.Cinema)
                .Include(m => m.MovieActors).ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movieDB == null) return NotFound();


            var movieDTO = mapper.Map<MovieDTO>(movieDB);
            // Que no se repitan los cines.
            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(x => x.Id).ToList();
            return movieDTO;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<MovieDTO>>> GetFilteredMovies([FromQuery] MovieFilterDTO movieFilterDTO)
        {
            var moviesQueryable = context.Movies.AsQueryable();
            if (!string.IsNullOrEmpty(movieFilterDTO.Title))
            {
                moviesQueryable = moviesQueryable.Where(m => m.Title.Contains(movieFilterDTO.Title));
            }
            if (movieFilterDTO.OnBillboard)
            {
                moviesQueryable = moviesQueryable.Where(m => m.OnBillboard);
            }
            if (movieFilterDTO.ComingSoon)
            {
                var today = DateTime.Now;
                moviesQueryable = moviesQueryable.Where(m => m.PremiereDate > today);
            }
            if (movieFilterDTO.GenreId != 0)
            {
                moviesQueryable = moviesQueryable.Where(m =>
                    m.Genres.Select(g => g.Id).Contains(movieFilterDTO.GenreId));
            }

            var movies = await moviesQueryable
                .Include(m => m.Genres)
                .ToListAsync();

            return mapper.Map<List<MovieDTO>>(movies);
        }


        [HttpPost]
        public async Task<ActionResult> Post(MovieCreationDTO movieCreationDTO)
        {
            Movie movie = mapper.Map<Movie>(movieCreationDTO);
            movie.Genres.ForEach(g => context.Entry(g).State = EntityState.Unchanged);
            movie.Auditoriums.ForEach(a => context.Entry(a).State = EntityState.Unchanged);

            if (movie.MovieActors is not null)
            {
                for (int i = 0; i < movie.MovieActors.Count; i++)
                {
                    movie.MovieActors[i].Order = i + 1;
                }
            }

            context.Add(movie);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromBody] MovieCreationDTO movieCreationDTO, int id)
        {
            var movieDB = await context.Movies
                .Include(m => m.MovieActors)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync();
            if (movieDB == null) return NotFound();

            movieDB = mapper.Map(movieCreationDTO, movieDB);

            await context.SaveChangesAsync();
            return NoContent();
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movieDB = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movieDB == null) return NotFound();

            context.Remove(movieDB);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }

}
