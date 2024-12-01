using System;

namespace mPole.Utils.Exceptions
{
    public class MoveException : Exception
    {
        public MoveException(string message) : base(message) { }
        public MoveException(string message, Exception innerException) : base(message, innerException) { }
    }
}