using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.Web.Utils.Exceptions
{
    public class InvalidAcceptDataException : Exception
    {
        public InvalidAcceptDataException() : base("Invalid accept data") { }
    }
}
