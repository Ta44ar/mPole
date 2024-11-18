namespace mPole.Data.Models
{
    public class Move
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public required int DifficultyLevel { get; set; }
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
