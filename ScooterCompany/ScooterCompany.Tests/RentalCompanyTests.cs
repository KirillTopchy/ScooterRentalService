using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ScooterCompany.Exceptions;
using ScooterCompany.Interfaces;
using ScooterCompany.Models;

namespace ScooterCompany.Tests
{
    public class RentalCompanyTests
    {
        private IRentalCompany _target;
        private IScooterService _scooterService;
        private IList<RentedScooter> _rentedScooters;
        private IRentalCalculator _calculator;
        private const string DefaultCompanyName = "Company";
        private const string DefaultId = "1";
        private const decimal DefaultPricePerMinute = 0.1m;

        [SetUp]
        public void Setup()
        {
            _scooterService = new ScooterService();
            _rentedScooters = new List<RentedScooter>();
            _calculator = new RentalCalculator();
            _target = new RentalCompany(DefaultCompanyName, _scooterService, _rentedScooters, _calculator);
        }

        [Test]
        public void CreateCompany_DefaultName_ShouldReturnDefaultName()
        {
            //Arrange
            _target = new RentalCompany(DefaultCompanyName, _scooterService, _rentedScooters, _calculator);

            //Assert
            Assert.AreEqual(DefaultCompanyName, _target.Name);
        }

        [Test]
        public void CreateCompany_NullName_ShouldThrowInvalidCompanyNameException()
        {
            //Assert
            Assert.Throws<InvalidCompanyNameException>(() => _target = new RentalCompany(null, _scooterService, _rentedScooters, _calculator));
        }

        [Test]
        public void CreateCompany_EmptyStringName_ShouldThrowInvalidCompanyNameException()
        {
            //Assert
            Assert.Throws<InvalidCompanyNameException>(() => _target = new RentalCompany("", _scooterService, _rentedScooters, _calculator));
        }

        [Test]
        public void StartRent_RentFirstScooter_ScooterShouldBeRented()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);

            //Act
            _target.StartRent(DefaultId);

            //Assert
            var actual = _scooterService.GetScooterById(DefaultId).IsRented;
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void StartRent_RentAlreadyRentedScooter_ShouldThrowScooterIsRentedException()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);

            //Act
            _target.StartRent(DefaultId);

            //Assert
            Assert.Throws<ScooterIsRentedException>(() =>_target.StartRent(DefaultId));
        }

        [Test]
        public void StartRent_RentNonExistingScooter_ShouldThrowScooterNotFoundException()
        {
            //Assert
            Assert.Throws<ScooterNotFoundException>(() => _target.StartRent(DefaultId));
        }

        [Test]
        public void StartRent_FirstScooterRented_RentedListShouldBeUpdated()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);

            //Act
            _target.StartRent(DefaultId);

            //Assert
            var rentedScooter = _rentedScooters.FirstOrDefault(s => s.Id == DefaultId);
            Assert.AreEqual(1, _rentedScooters.Count);
            Assert.AreEqual(DefaultPricePerMinute, rentedScooter.Price);
        }

        [Test]
        public void EndRent_EndingRentForRentedScooter_ShouldPass()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);
            _scooterService.GetScooterById(DefaultId).IsRented = true;
            var startRent = DateTime.UtcNow.AddMinutes(-20);
            var endRent = DateTime.UtcNow;
            _rentedScooters.Add(new RentedScooter
            {
                Id = DefaultId,
                RentStarted = startRent,
                RentFinished = endRent,
                Price = DefaultPricePerMinute
            });

            //Act
            var result = _target.EndRent(DefaultId);

            //Assert
            Assert.GreaterOrEqual(result, 0);
        }

        [Test]
        public void EndRent_EndingRentForNotRentedScooter_ShouldThrowScooterNotRentedException()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);

            //Assert
            Assert.Throws<ScooterNotRentedException>(() => _target.EndRent(DefaultId));
        }

        [Test]
        public void EndRent_FirstScooterRentEnded_RentedListShouldBeUpdated()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);
            _scooterService.GetScooterById(DefaultId).IsRented = true;
            var startRent = DateTime.UtcNow.AddMinutes(-20);
            var endRent = DateTime.UtcNow;
            _rentedScooters.Add(new RentedScooter
            {
                Id = DefaultId,
                RentStarted = startRent,
                RentFinished = endRent,
                Price = DefaultPricePerMinute
            });

            //Act
            _target.EndRent(DefaultId);

            //Assert
            var rentedScooter = _rentedScooters.FirstOrDefault(s => s.Id == DefaultId);
            Assert.AreEqual(1, _rentedScooters.Count);
            Assert.NotNull(rentedScooter.RentFinished);
        }

        [Test]
        public void EndRent_ScooterRented20Minutes_ShouldReturn2()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);
            _scooterService.GetScooterById(DefaultId).IsRented = true;
            var startRent = DateTime.UtcNow.AddMinutes(-20);
            var endRent = DateTime.UtcNow;
            _rentedScooters.Add(new RentedScooter
            {
                Id = DefaultId,
                RentStarted = startRent,
                RentFinished = endRent,
                Price = DefaultPricePerMinute
            });

            //Act
            var result = _target.EndRent(DefaultId);

            //Assert
            Assert.AreEqual(2.00m, result);
        }

        [Test]
        public void EndRent_ScooterRented4Hours_ShouldReturn20()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);
            _scooterService.GetScooterById(DefaultId).IsRented = true;
            var startRent = DateTime.UtcNow.AddHours(-4);
            var endRent = DateTime.UtcNow;
            _rentedScooters.Add(new RentedScooter
            {
                Id = DefaultId,
                RentStarted = startRent,
                RentFinished = endRent,
                Price = DefaultPricePerMinute
            });

            //Act
            var result = _target.EndRent(DefaultId);

            //Assert
            Assert.AreEqual(20.00m, result);
        }

        [Test]
        public void EndRent_ScooterRentedOnAPreviousDayReturnedOnANextDayAndTotalHoursIs1_ShouldReturn6()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);
            _scooterService.GetScooterById(DefaultId).IsRented = true;
            var startRent = new DateTime(2022, 2, 20, 23, 50, 0);
            var endRent = new DateTime(2022, 2, 21, 0, 50, 0);
            _rentedScooters.Add(new RentedScooter
            {
                Id = DefaultId,
                RentStarted = startRent,
                Price = DefaultPricePerMinute,
                RentFinished = endRent
            });

            //Act
            var result = _target.EndRent(DefaultId);

            //Assert
            Assert.AreEqual(6.00m, result);
        }

        [Test]
        public void EndRent_ScooterRentedOnAPreviousDayReturnedOnANextDayAndTotalHoursIs5_ShouldReturn26()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);
            _scooterService.GetScooterById(DefaultId).IsRented = true;
            var startRent = new DateTime(2022, 2, 20, 20, 00, 0);
            var endRent = new DateTime(2022, 2, 21, 1, 00, 0);
            _rentedScooters.Add(new RentedScooter
            {
                Id = DefaultId,
                RentStarted = startRent,
                Price = DefaultPricePerMinute,
                RentFinished = endRent
            });

            //Act
            var result = _target.EndRent(DefaultId);

            //Assert
            Assert.AreEqual(26.00m, result);
        }

        [Test]
        public void EndRent_ScooterRented4DaysLastDayUsed1Hour_ShouldReturn66()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);
            _scooterService.GetScooterById(DefaultId).IsRented = true;
            var startRent = new DateTime(2022, 2, 20, 20, 00, 0);
            var endRent = new DateTime(2022, 2, 23, 1, 00, 0);
            _rentedScooters.Add(new RentedScooter
            {
                Id = DefaultId,
                RentStarted = startRent,
                Price = DefaultPricePerMinute,
                RentFinished = endRent
            });

            //Act
            var result = _target.EndRent(DefaultId);

            //Assert
            Assert.AreEqual(66.00m, result);
        }

        [Test]
        public void EndRent_ScooterRented4DaysLastDayUsed10Hour_ShouldReturn80()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);
            _scooterService.GetScooterById(DefaultId).IsRented = true;
            var startRent = new DateTime(2022, 2, 20, 20, 00, 0);
            var endRent = new DateTime(2022, 2, 23, 10, 00, 0);
            _rentedScooters.Add(new RentedScooter
            {
                Id = DefaultId,
                RentStarted = startRent,
                Price = DefaultPricePerMinute,
                RentFinished = endRent
            });

            //Act
            var result = _target.EndRent(DefaultId);

            //Assert
            Assert.AreEqual(80.00m, result);
        }
    }
}
