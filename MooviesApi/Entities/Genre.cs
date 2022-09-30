using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<MovieGenre> MoviesGenres { get; set; }
    }
}
