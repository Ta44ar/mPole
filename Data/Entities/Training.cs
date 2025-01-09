using System.ComponentModel.DataAnnotations;

namespace mPole.Data.Models
{
    public class Training
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public virtual ICollection<Move> Moves { get; set; } = new List<Move>();
        public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}