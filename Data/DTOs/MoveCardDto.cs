namespace mPole.Data.DTOs
{
    public class MoveCardDto
    {
        public int Id { get; set; }
        public required string ImageUrl { get; set; }
        public required string Name { get; set; }
        public int DifficultyLevel { get; set; }
    }
}