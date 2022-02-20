using System;
using System.Collections.Generic;
using System.Linq;
using ScooterCompany.Exceptions;
using ScooterCompany.Interfaces;

namespace ScooterCompany.Models
{
    public class RentalCompany : IRentalCompany
    {
        public RentalCompany(string name, IScooterService service, IList<RentedScooter> archive, IRentalCalculator calculator)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidCompanyNameException();
            }
            
            Name = name;
            _scooterService = service;
            _rentedScooters = archive;
            _calculator = calculator;
        }

        private readonly IScooterService _scooterService;
        private readonly IList<RentedScooter> _rentedScooters;
        private readonly IRentalCalculator _calculator;

        public string Name { get; }

        public void StartRent(string id)
        { 
            var scooter = _scooterService.GetScooterById(id);
            if (scooter.IsRented)
            {
                throw new ScooterIsRentedException();
            }
            scooter.IsRented = true;
            _rentedScooters.Add(new RentedScooter
            {
                Id = scooter.Id,
                Price = scooter.PricePerMinute,
                RentStarted = DateTime.UtcNow,
                RentFinished = null
            });
        }

        public decimal EndRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            if (!scooter.IsRented)
            {
                throw new ScooterNotRentedException();
            }

            scooter.IsRented = false;
            var rented = (_rentedScooters.FirstOrDefault(s => s.Id == id && !s.RentFinished.HasValue));
            rented.RentFinished = DateTime.UtcNow;

            return _calculator.CalculateRent(rented);
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            throw new NotImplementedException();
        }
    }
}
