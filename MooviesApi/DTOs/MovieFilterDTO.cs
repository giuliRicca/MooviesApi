namespace MoviesApi.DTOs
{
    public class MovieFilterDTO
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public bool OnBillboard { get; set; }
        public bool ComingSoon { get; set; }
    }
}
