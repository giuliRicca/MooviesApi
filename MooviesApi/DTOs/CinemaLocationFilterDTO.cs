namespace MoviesApi.DTOs
{
    public class CinemaLocationFilterDTO
    {
        public double latitude { get;set;}
        public double longitude { get;set;}

        public int distance { get; set; }
    }
}
