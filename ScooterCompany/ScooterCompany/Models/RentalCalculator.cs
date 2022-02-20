using System;
using ScooterCompany.Interfaces;


namespace ScooterCompany.Models
{
    public class RentalCalculator : IRentalCalculator
    {

        private readonly decimal _maxPricePerDay = 20.0m;

        public decimal CalculateRent(RentedScooter scooter)
        {
            var rentTime = (TimeSpan)(scooter.RentFinished - scooter.RentStarted);

            // If scooter rent was started and finished at the same day.
            if (Math.Floor(rentTime.TotalDays) == 0 && scooter.RentStarted.Date == Convert.ToDateTime(scooter.RentFinished).Date)
            {
                var totalPrice = Math.Round((decimal)rentTime.TotalMinutes * scooter.Price,2);

                return CheckIfRentPriceForOneDayIsLessThanMaxPricePerDay(totalPrice);
            }

            // If scooter rent start and finish days are different, but total rent time is less than 24 hours.
            if (Math.Floor(rentTime.TotalDays) == 0 && scooter.RentStarted.Date != Convert.ToDateTime(scooter.RentFinished).Date)
            {
                var firstDayRentalTime = TimeSpan.FromDays(1) - scooter.RentStarted.TimeOfDay;
                var firstDayRentalPrice = Math.Round((decimal)firstDayRentalTime.TotalMinutes * scooter.Price, 2);

                var secondDay = scooter.RentFinished.Value.TimeOfDay;
                var secondDayRentalPrice = Math.Round((decimal)secondDay.TotalMinutes * scooter.Price, 2);

                var totalPriceForFirstDay = CheckIfRentPriceForOneDayIsLessThanMaxPricePerDay(firstDayRentalPrice);
                var totalPriceForSecondDay = CheckIfRentPriceForOneDayIsLessThanMaxPricePerDay(secondDayRentalPrice);

                return totalPriceForFirstDay + totalPriceForSecondDay; 
            }

            // If total rent time is more than 24 hours.
            return CalculateMultipleDaysRent(scooter);
        }

        public decimal CheckIfRentPriceForOneDayIsLessThanMaxPricePerDay(decimal totalPrice)
        {
            return totalPrice > _maxPricePerDay ? _maxPricePerDay : totalPrice;
        }

        public decimal CalculateMultipleDaysRent(RentedScooter scooter)
        {
            var fullDaysCounter = 0;
            var temporaryDate = scooter.RentStarted;

            while (temporaryDate < scooter.RentFinished)
            {
                temporaryDate = temporaryDate.AddDays(1);
                fullDaysCounter++;
            }

            var lastDay = scooter.RentFinished.Value.TimeOfDay;
            var lastDayRentalPrice = Math.Round((decimal)lastDay.TotalMinutes * scooter.Price, 2);
            var totalPriceForLastDay = CheckIfRentPriceForOneDayIsLessThanMaxPricePerDay(lastDayRentalPrice);
            var totalPrice = Math.Round((decimal)fullDaysCounter * 20 + totalPriceForLastDay,2);
            return totalPrice;
        }
    }
}
