using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using ScooterCompany.Exceptions;
using ScooterCompany.Interfaces;

namespace ScooterCompany.Models
{
    public class ScooterService : IScooterService
    {
        private readonly List<Scooter> _scooterList;

        public ScooterService()
        {
            _scooterList = new List<Scooter>();
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (pricePerMinute <= 0)
            {
                throw new InvalidPriceException();
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidIdException();
            }

            if (_scooterList.Any(s => s.Id == id))
            {
                throw new IdAlreadyUsedException();
            }

            var scooter = new Scooter(id, pricePerMinute);
            _scooterList.Add(scooter);
        }

        public void RemoveScooter(string id)
        {
            if (_scooterList.All(s => s.Id != id))
            {
                throw new ScooterNotFoundException();
            }

            _scooterList.Remove(_scooterList.First(s => s.Id == id));
        }

        public IList<Scooter> GetScooters()
        {
            return _scooterList.ToList();
        }

        public Scooter GetScooterById(string scooterId)
        {
            var scooter = _scooterList.FirstOrDefault(s => s.Id == scooterId);
            if (scooter == null)
            {
                throw new ScooterNotFoundException();
            }

            return scooter;
        }
    }
}
