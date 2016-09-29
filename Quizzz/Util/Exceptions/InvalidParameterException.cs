using System;

namespace Quizzz.Util.Exceptions
{
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException() : base("Invalid parameter") { }
        public InvalidParameterException(string message) : base(message) { }
    }
}