namespace mPole.Data.Models
{
    public class PoleDanceMove
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Level { get; set; }
        public ICollection<PoleDanceMoveImage> PoleDanceMoveImages { get; set; } = new List<PoleDanceMoveImage>();
    }
}
