using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOs;
using MoviesApi.Entities;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresControllers : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public GenresControllers(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {
            List<Genre> genresDB = await context.Genres.ToListAsync();
            List<GenreDTO> genreDTOs = mapper.Map<List<GenreDTO>>(genresDB);
            return genreDTOs;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreDTO>> Get(int id)
        {
            var genreDB = await context.Genres.FirstOrDefaultAsync(genreDB => genreDB.Id == id);

            if (genreDB == null) return NotFound();

            GenreDTO genreDTO = mapper.Map<GenreDTO>(genreDB);
            return genreDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genreNameAlreadyExists = await context.Genres.AnyAsync(genreDb => genreDb.Name == genreCreationDTO.Name);
            if (genreNameAlreadyExists) return BadRequest($"The genre {genreCreationDTO.Name} already exists.");

            Genre newGenre = mapper.Map<Genre>(genreCreationDTO);
            context.Add(newGenre);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genreDB = await context.Genres.FirstOrDefaultAsync(g => g.Id == id);
            if (genreDB == null) return NotFound();

            genreDB = mapper.Map<Genre>(genreCreationDTO);
            context.Add(genreDB);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
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
