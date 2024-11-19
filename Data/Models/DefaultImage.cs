using System.IO;

namespace mPole.Data.Models
{
    public class DefaultImage : Image
    {
        public new string Name { get; set; }
        public new byte[] ImageData { get; set; }

        public DefaultImage()
        {
            Name = "No image added yet";
            ImageData = LoadDefaultImage();
        }

        private byte[] LoadDefaultImage()
        {
            string filePath = "wwwroot/images/default image.png";
            return File.Exists(filePath) ? File.ReadAllBytes(filePath) : Array.Empty<byte>();
        }
    }
}
