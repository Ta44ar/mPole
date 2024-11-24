using System.IO;

namespace mPole.Data.Models
{
    public class DefaultImage : Image
    {
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
