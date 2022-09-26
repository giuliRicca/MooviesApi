using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace MoviesApi.Entities
{
    public class Auditorium
    {
        public int Id { get; set; }
        public AuditoriumType AuditoriumType { get; set; }
        public decimal Price { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        public HashSet<Movie> Movies { get; set; }
    }
}
