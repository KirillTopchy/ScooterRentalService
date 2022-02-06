using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterCompany.Exceptions
{
    public class InvalidCompanyNameException : System.Exception
    {
        public InvalidCompanyNameException() : base("Invalid Company name provided")
        {

        }

        public InvalidCompanyNameException(string message) : base(message)
        {
            
        }

        public InvalidCompanyNameException(string message, System.Exception exception) : base(message, exception)
        {
            
        }
    }
}
