using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterCompany.Exceptions
{
    public class ScooterNotFoundException : System.Exception
    {
        public ScooterNotFoundException() : base ("Scooter not found")
        {
            
        }

        public ScooterNotFoundException(string message) : base(message)
        {
           
        }

        public ScooterNotFoundException(string message, System.Exception exception) : base(message, exception)
        {

        }
    }
}
