using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTOs
{
    public class GenreCreationDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
