using System;
using ScooterCompany.Interfaces;


namespace ScooterCompany.Models
{
    public class RentalCalculator : IRentalCalculator
    {
        public decimal CalculateRent(RentedScooter scooter)
        {
            var time = scooter.RentFinished - scooter.RentStarted;
            return Math.Round((decimal) time.Value.TotalMinutes * scooter.Price, MidpointRounding.ToEven);
        }
    }
}
