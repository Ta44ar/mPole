using mPole.Data.Models;
using System.ComponentModel.DataAnnotations;

public class Training
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Type { get; set; }
    public string? ImageUrl { get; set; }
    public virtual ICollection<Move> Moves { get; set; } = new List<Move>();
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}