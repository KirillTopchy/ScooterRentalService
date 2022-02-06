using System;
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
        private const string DefaultCompanyName = "Company";
        private const string DefaultId = "1";
        private const decimal DefaultPricePerMinute = 0.20m;

        [SetUp]
        public void Setup()
        {
            _scooterService = new ScooterService();
            _target = new RentalCompany(DefaultCompanyName, _scooterService);
        }

        [Test]
        public void CreateCompany_DefaultName_ShouldReturnDefaultName()
        {
            //Arrange
            _target = new RentalCompany(DefaultCompanyName, _scooterService);

            //Assert
            Assert.AreEqual(DefaultCompanyName, _target.Name);
        }

        [Test]
        public void CreateCompany_NullName_ShouldThrowInvalidCompanyNameException()
        {
            //Assert
            Assert.Throws<InvalidCompanyNameException>(() => _target = new RentalCompany(null, _scooterService));
        }

        [Test]
        public void CreateCompany_EmptyStringName_ShouldThrowInvalidCompanyNameException()
        {
            //Assert
            Assert.Throws<InvalidCompanyNameException>(() => _target = new RentalCompany("", _scooterService));
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
        public void EndRent_EndingRentForRentedScooter_ShouldPass()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);
            _target.StartRent(DefaultId);

            //Act
            var result = _target.EndRent(DefaultId);

            //Assert
            Assert.Greater(result, 0);
        }

        [Test]
        public void EndRent_EndingRentForNotRentedScooter_ShouldThrowScooterNotRentedException()
        {
            //Arrange
            _scooterService.AddScooter(DefaultId, DefaultPricePerMinute);

            //Assert
            Assert.Throws<ScooterNotRentedException>(() => _target.EndRent(DefaultId));
        }
    }
}
