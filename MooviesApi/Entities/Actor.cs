using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime? Birthday { get; set; }
        public HashSet<MovieActor> MovieActors { get; set; }
        [NotMapped]
        public int? age
        {
            get
            {
                if (!Birthday.HasValue) return null;
                var birthdayDate = Birthday.Value;
                var age = DateTime.Now.Year - birthdayDate.Year;
                if (new DateTime(birthdayDate.Year, birthdayDate.Month, birthdayDate.Day) > DateTime.Today)
                {
                    age--;
                }
                return age;
            }
        }
    }
}
