using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzzClient.DataAccess.Exceptions
{
    public class DatabaseConnectException : Exception
    {
        public DatabaseConnectException() : base("Error during access db") { }
    }
}
