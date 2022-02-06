using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterCompany.Exceptions
{
    public class ScooterIsRentedException : System.Exception
    {
        public ScooterIsRentedException(): base("Scooter is rented")
        {
            
        }

        public ScooterIsRentedException(string message) : base(message)
        {
            
        }

        public ScooterIsRentedException(string message, System.Exception exception) : base(message, exception)
        {
            

        }
    }
}
