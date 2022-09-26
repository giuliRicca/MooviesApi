using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using MoviesApi.DTOs;
using MoviesApi.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/cinemas")]
    public class CinemasController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public CinemasController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CinemaDTO>>> Get()
        {
            var cinemasDB = await context.Cinemas.ToListAsync();
            return mapper.Map<List<CinemaDTO>>(cinemasDB);
        }

        [HttpGet("closeToMe")]
        public async Task<ActionResult<List<CinemaDTO>>> Get([FromQuery] double latitude, double longitude)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            int maxDistance = 2000;
            var myLocation = geometryFactory.CreatePoint(new Coordinate(longitude, latitude));
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

        [HttpGet("{id:int}")]
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

        [HttpPost]
        public async Task<ActionResult> Post(CinemaCreationDTO cinemaCreationDTO)
        {
            var cinemaNameTaken = await context.Cinemas.AnyAsync(c => c.Name.Contains(cinemaCreationDTO.Name));
            if (cinemaNameTaken) return BadRequest("Cinema Name already taken");

            var cinema = mapper.Map<Cinema>(cinemaCreationDTO);
            context.Add(cinema);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
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
