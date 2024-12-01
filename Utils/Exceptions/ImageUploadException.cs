using System;

namespace mPole.Utils.Exceptions
{
    public class ImageUploadException : Exception
    {
        public ImageUploadException(string message) : base(message) { }
        public ImageUploadException(string message, Exception innerException) : base(message, innerException) { }
    }
}