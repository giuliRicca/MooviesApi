namespace MoviesApi.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool OnBillboard { get; set; }
        public DateTime PremiereDate { get; set; }
        public string PosterUrl { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Auditorium> Auditoriums { get; set; }
        public List<MovieActor> MovieActors { get; set; }

    }
}
