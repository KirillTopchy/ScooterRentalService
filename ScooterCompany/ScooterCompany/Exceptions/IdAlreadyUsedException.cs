using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterCompany.Exceptions
{
    public class IdAlreadyUsedException : System.Exception
    {
        public IdAlreadyUsedException() : base("Scooter with same ID already exists")
        {
            
        }

        public IdAlreadyUsedException(string message) : base(message)
        {
            
        }

        public IdAlreadyUsedException(string message, System.Exception exception) : base(message, exception)
        {
            
        }
    }
}
