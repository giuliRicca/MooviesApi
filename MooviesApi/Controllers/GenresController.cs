using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOs;
using MoviesApi.Entities;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public GenresController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet(Name = "getGenres")]
        public async Task<ActionResult<List<GenreDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {

            List<Genre> genresDB = await context.Genres
                .Skip((paginationDTO.PageIndex - 1) * paginationDTO.PageSize)
                .Take(paginationDTO.PageSize)
                .ToListAsync();
            List<GenreDTO> genreDTOs = mapper.Map<List<GenreDTO>>(genresDB);
            return genreDTOs;
        }

        [HttpGet("{id:int}", Name = "getGenre")]
        public async Task<ActionResult<GenreDTO>> Get(int id)
        {
            var genreDB = await context.Genres.FirstOrDefaultAsync(genreDB => genreDB.Id == id);

            if (genreDB == null) return NotFound();

            GenreDTO genreDTO = mapper.Map<GenreDTO>(genreDB);
            return genreDTO;
        }

        [HttpPost(Name = "createGenre")]
        public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genreNameAlreadyExists = await context.Genres.AnyAsync(genreDb => genreDb.Name == genreCreationDTO.Name);
            if (genreNameAlreadyExists) return BadRequest($"The genre {genreCreationDTO.Name} already exists.");

            Genre newGenre = mapper.Map<Genre>(genreCreationDTO);
            context.Add(newGenre);
            await context.SaveChangesAsync();
            var genreDTO = mapper.Map<GenreDTO>(newGenre);
            return new CreatedAtRouteResult("getGenre", new {id = genreDTO.Id}, genreDTO);
        }

        [HttpPut("{id:int}", Name = "updateGenre")]
        public async Task<ActionResult> Put(int id, [FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genre = mapper.Map<Genre>(genreCreationDTO);
            genre.Id = id;
            context.Update(genre);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "deleteGenre")]
        public async Task<ActionResult> Delete(int id)
        {
            var genreDB = await context.Genres.FirstOrDefaultAsync(g => g.Id == id);
            if (genreDB == null) return NotFound();

            context.Remove(genreDB);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
