namespace mPole.Data.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[] ImageData { get; set; }
        public int MoveId { get; set; }
        public virtual Move? Move { get; set; }
    }
}
