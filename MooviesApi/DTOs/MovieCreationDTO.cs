namespace MoviesApi.DTOs
{
    public class MovieCreationDTO
    {
        public string Title { get; set; }
        public bool OnBillboard { get; set; }
        public DateTime PremiereDate { get; set; }
        public List<int> GenresIds { get; set; }
        public List<int> AuditoriumsIds { get; set; }
        public List<MovieActorCreationDTO> MovieActorCreationDTOs { get; set; }
    }
}
