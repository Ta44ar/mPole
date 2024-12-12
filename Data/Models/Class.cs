using System.ComponentModel.DataAnnotations;

namespace mPole.Data.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public required string Duration { get; set; }
        public required DateTime Date { get; set; }
        public required string Location { get; set; }
        public required string Trainer { get; set; }
        public int TrainingId { get; set; }
        public Training Training { get; set; } = null!;
        public IList<string> RegisteredUsers { get; set; } = new List<string>(); //w postaci lsity username'ów starczy? nie chce całych userów, żeby uniknąć joinowej tabeli
    }
}
