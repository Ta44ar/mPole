namespace mPole.Data.Models
{
    public class Move
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DifficultyLevel { get; set; }
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
