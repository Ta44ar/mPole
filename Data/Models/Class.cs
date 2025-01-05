using System.ComponentModel.DataAnnotations;

namespace mPole.Data.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public double Duration { get; set; }
        public DateTime? Date { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Trainer { get; set; } = string.Empty;
        public int TrainingId { get; set; }
        public virtual Training Training { get; set; } = null!;
        public virtual IList<ApplicationUser> RegisteredUsers { get; set; } = new List<ApplicationUser>();
    }
}
