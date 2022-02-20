using System;
using ScooterCompany.Interfaces;


namespace ScooterCompany.Models
{
    public class RentalCalculator : IRentalCalculator
    {

        private readonly decimal _maxPricePerDay = 20.0m;

        public decimal CalculateRent(RentedScooter scooter)
        {
            decimal totalPrice = 0;
            var rentTime = (TimeSpan)(scooter.RentFinished - scooter.RentStarted);

            // If scooter rent was started and finished at the same day within 24 hours.
            if (Math.Floor(rentTime.TotalDays) == 0 && scooter.RentStarted.Date == Convert.ToDateTime(scooter.RentFinished).Date)
            {
                totalPrice = (decimal)rentTime.TotalMinutes * scooter.Price;

                return CheckIfRentPriceForOneDayIsLessThanMaxPricePerDay(totalPrice);
            }

            // If scooter rent start and finish days are different, but total rent time is less than 24 hours.
            else if (rentTime.TotalDays == 0)
            {
                totalPrice = (decimal)rentTime.TotalMinutes * scooter.Price;
                return CheckIfRentPriceForOneDayIsLessThanMaxPricePerDay(totalPrice);
            }
            else
            {
               return CheckIfRentPriceForOneDayIsLessThanMaxPricePerDay(totalPrice);
            }
        }

        public decimal CheckIfRentPriceForOneDayIsLessThanMaxPricePerDay(decimal totalPrice)
        {
            return totalPrice > _maxPricePerDay ? _maxPricePerDay : totalPrice;
        }

        public decimal CalculateRentForMultipleDays(RentedScooter scooter)
        {
            return 1.0m;
        }
    }
}
