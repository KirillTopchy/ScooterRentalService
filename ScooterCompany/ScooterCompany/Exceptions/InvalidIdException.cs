using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterCompany.Exceptions
{
    public class InvalidIdException : System.Exception
    {
        public InvalidIdException() : base("Invalid id provided")    
        {
            
        }

        public InvalidIdException(string message) : base(message)
        {
            
        }

        public InvalidIdException(string message, System.Exception exception) : base(message, exception)
        {
            
        }
    }
}
