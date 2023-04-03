using System;

namespace SimCovidAPI.Exceptions
{
    public class InvalidISpreadableException : Exception
    {

        public InvalidISpreadableException()
        {
        }

        public InvalidISpreadableException(string message) : base(message)
        {
            
        }

        public InvalidISpreadableException(string message, Exception innerException) : base(message, innerException)
        {
            throw innerException;
        }
    }
}