using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTOs
{
    public class PaginationDTO
    {
        public int PageIndex { get; set; } = 1;
        [Range(1, 20)]
        public int PageSize { get; set; } = 5;
    }
}
