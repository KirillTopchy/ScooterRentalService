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
        private string _defaultName = "Company";
        private const string Id = "1";
        private const decimal PricePerMinute = 0.20m;

        [SetUp]
        public void Setup()
        {
            _scooterService = new ScooterService();
            _target = new RentalCompany(_defaultName, _scooterService);
        }

        [Test]
        public void CreateCompany_DefaultName_ShouldReturnDefaultName()
        {
            //Arrange
            _target = new RentalCompany(_defaultName, _scooterService);

            //Assert
            Assert.AreEqual(_defaultName, _target.Name);
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
            _scooterService.AddScooter(Id, PricePerMinute);

            //Act
            _target.StartRent(Id);

            //Assert
            var actual = _scooterService.GetScooterById(Id).IsRented;
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void StartRent_RentAlreadyRentedScooter_ShouldThrowScooterIsRentedException()
        {
            //Arrange
            _scooterService.AddScooter(Id, PricePerMinute);

            //Act
            _target.StartRent(Id);

            //Assert
            Assert.Throws<ScooterIsRentedException>(() =>_target.StartRent(Id));
        }
    }
}
