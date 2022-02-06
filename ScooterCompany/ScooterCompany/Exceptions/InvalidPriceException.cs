
using System;

namespace ScooterCompany.Exceptions
{
    public class InvalidPriceException : System.Exception
    {
        public InvalidPriceException() : base("Invalid price provided")
        {
            
        }

        public InvalidPriceException(string message) : base(message)
        {
            
        }

        public InvalidPriceException(string message, System.Exception exception) : base(message, exception)
        {
            
        }
    }
}
