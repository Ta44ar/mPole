using System.ComponentModel.DataAnnotations;

namespace mPole.Data.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public string Location { get; set; } = string.Empty;
        public int MaxParticipantsCount { get; set; }
        public int TrainingId { get; set; }
        public virtual Training Training { get; set; } = new();
        public string TrainerId { get; set; } = string.Empty;
        public virtual ApplicationUser Trainer { get; set; } = new();
        public virtual ICollection<Registration> Registrations { get; set; } = default!;
        public bool IsRegistrationOpen { get; set; } = true;

        public DateTime? DateTime
        {
            get
            {
                if (Date.HasValue && Time.HasValue)
                {
                    return Date.Value.Date + Time.Value;
                }
                return null;
            }
        }

        public Class()
        {
            MaxParticipantsCount = 10;
        }
    }
}