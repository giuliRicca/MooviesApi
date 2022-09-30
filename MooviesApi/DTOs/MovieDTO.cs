﻿namespace MoviesApi.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool OnBillboard { get;set; }
        public ICollection<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
        public ICollection<CinemaDTO> Cinemas { get; set; }
        public ICollection<ActorDTO> Actors { get; set; }
    }
}
