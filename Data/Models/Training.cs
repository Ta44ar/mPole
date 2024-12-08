using System.ComponentModel.DataAnnotations;

namespace mPole.Data.Models
{
    public class Training
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Type { get; set; }
        public required string Duration { get; set; }
        public required DateTime Date { get; set; }
        public required string Location { get; set; }
        public required string Trainer { get; set; }
        public string? ImageUrl { get; set; }
        public IList<string> RegisteredUsers { get; set; } = new List<string>();
        public IList<Move> Moves { get; set; } = new List<Move>();
    }
}
