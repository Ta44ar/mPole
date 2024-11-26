public class MoveDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DifficultyLevel { get; set; }
    public List<string> ImageUrls { get; set; } = new List<string>();
}