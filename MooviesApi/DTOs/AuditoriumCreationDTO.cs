using MoviesApi.Entities;

namespace MoviesApi.DTOs
{
    public class AuditoriumCreationDTO
    {
        public AuditoriumType AuditoriumType { get; set; }
        public decimal Price { get; set; }
    }
}
