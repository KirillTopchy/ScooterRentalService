using System;

namespace ScooterCompany.Models
{
    public class RentedScooter
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public DateTime RentStarted { get; set; }
        public DateTime? RentFinished { get; set; }
    }
}
