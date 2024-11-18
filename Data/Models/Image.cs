namespace mPole.Data.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public required byte[] ImageData { get; set; }
        public virtual Move? Move { get; set; }
    }
}
