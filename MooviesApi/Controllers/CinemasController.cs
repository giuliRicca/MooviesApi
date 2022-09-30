using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOs;
using MoviesApi.Entities;
using NetTopologySuite.Geometries;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/cinemas")]
    public class CinemasController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly GeometryFactory geometryFactory;

        public CinemasController(ApplicationDBContext context, IMapper mapper,
            GeometryFactory geometryFactory)
        {
            this.context = context;
            this.mapper = mapper;
            this.geometryFactory = geometryFactory;
        }

        [HttpGet(Name = "getCinemas")]
        public async Task<ActionResult<List<CinemaDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var cinemasDB = await context.Cinemas
                .Skip((paginationDTO.PageIndex - 1) * paginationDTO.PageSize)
                .Take(paginationDTO.PageSize)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return mapper.Map<List<CinemaDTO>>(cinemasDB);
        }

        [HttpGet("nearBy", Name = "getCinemasNearby")]
        public async Task<ActionResult<List<Cinema>>> Nearby([FromQuery] CinemaLocationFilterDTO cinemaLocationFilterDTO)
        {
            int maxDistance = cinemaLocationFilterDTO.distance * 1000;
            var myLocation = geometryFactory.CreatePoint(new Coordinate(cinemaLocationFilterDTO.longitude, cinemaLocationFilterDTO.latitude));
            var cinemas = await context.Cinemas
                .OrderBy(c => c.Location.Distance(myLocation))
                .Where(c => c.Location.IsWithinDistance(myLocation, maxDistance))
                .Select(c => new 
                {
                    Name = c.Name,
                    Distance = Math.Round(c.Location.Distance(myLocation))
                }).ToListAsync();
            
            return Ok(cinemas);
        }

        [HttpGet("{id:int}", Name = "getCinemaById")]
        public async Task<ActionResult> Get(int id)
        {
            var cinemaDB = await context.Cinemas
                .Include(c=>c.Auditoriums)
                .Include(c=>c.CinemaOffer)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cinemaDB == null) return NotFound();

            cinemaDB.Location = null;

            return Ok(cinemaDB);
        }

        [HttpPost(Name = "createCinema")]
        public async Task<ActionResult> Post(CinemaCreationDTO cinemaCreationDTO)
        {
            var cinemaNameTaken = await context.Cinemas.AnyAsync(c => c.Name.Contains(cinemaCreationDTO.Name));
            if (cinemaNameTaken) return BadRequest("Cinema Name already taken");

            var cinema = mapper.Map<Cinema>(cinemaCreationDTO);
            context.Add(cinema);
            await context.SaveChangesAsync();

            var cinemaDTO = mapper.Map<CinemaDTO>(cinema);
            return new CreatedAtRouteResult("getCinemaById", new { id = cinemaDTO.Id }, cinemaDTO);
        }

        [HttpPut("{id:int}", Name = "updateCinema")]
        public async Task<ActionResult> Put(int id, CinemaCreationDTO cinemaCreationDTO)
        {
            var cinemaDB = await context.Cinemas
                .Include(c => c.CinemaOffer)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cinemaDB == null) return NotFound();
            cinemaDB = mapper.Map(cinemaCreationDTO, cinemaDB);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "deleteCinema")]
        public async Task<ActionResult> Delete(int id)
        {
            var cinemaDB = await context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
            if (cinemaDB == null) return NotFound();

            context.Remove(cinemaDB);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
