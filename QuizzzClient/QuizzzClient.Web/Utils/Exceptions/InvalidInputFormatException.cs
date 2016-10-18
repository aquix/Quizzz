using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.Web.Utils.Exceptions
{
    public class InvalidInputFormatException : Exception
    {
        public InvalidInputFormatException(string data) : base("Data is not in JSON or XML format") {
            InvalidDataString = data;
        }

        public string InvalidDataString { get; set; }
    }
}
