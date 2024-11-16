namespace mPole.Data.Models
{
    public class PoleDanceMoveImage
    {
        public int PoleDanceMoveId { get; set; }
        public required PoleDanceMove PoleDanceMove { get; set; }
        
        public int ImageId { get; set; }
        public required Image Image { get; set; }
    }
}
