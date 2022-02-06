using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScooterCompany.Exceptions;
using ScooterCompany.Interfaces;

namespace ScooterCompany.Models
{
    public class RentalCompany : IRentalCompany
    {
        public RentalCompany(string name, IScooterService service)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidCompanyNameException();
            }
            
            Name = name;
            _scooterService = service;
        }

        private readonly IScooterService _scooterService;

        public string Name { get; }

        public void StartRent(string id)
        { 
            var scooter = _scooterService.GetScooterById(id);
            if (scooter.IsRented)
            {
                throw new ScooterIsRentedException();
            }
            scooter.IsRented = true;
        }

        public decimal EndRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            if (!scooter.IsRented)
            {
                throw new ScooterNotRentedException();
            }

            scooter.IsRented = false;

            return scooter.PricePerMinute;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            throw new NotImplementedException();
        }
    }
}
