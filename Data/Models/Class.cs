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
        public virtual Training Training { get; set; } = null!;
        public virtual IList<ApplicationUser> RegisteredUsers { get; set; } = new List<ApplicationUser>();
    }
}
