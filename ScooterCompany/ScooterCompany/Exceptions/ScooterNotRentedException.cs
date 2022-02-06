using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterCompany.Exceptions
{
    public class ScooterNotRentedException : System.Exception
    {
        public ScooterNotRentedException() : base("Scooter is not rented")
        {
            
        }

        public ScooterNotRentedException(string message) : base(message)
        {
            
        }

        public ScooterNotRentedException(string message, System.Exception exception) : base(message, exception)
        {
            
        }
    }
}
