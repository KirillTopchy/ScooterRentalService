 using NUnit.Framework;
 using ScooterCompany.Exceptions;
 using ScooterCompany.Interfaces;
 using ScooterCompany.Models;

 namespace ScooterCompany.Tests
{
    public class ScooterServiceTests
    {
        private IScooterService _target;

        [SetUp]
        public void Setup()
        {
            _target = new ScooterService();
        }

        [Test]
        public void AddScooter_1_020_ScooterAdded()
        {
            //Arrange 
            var id = "1";
            var pricePerMinute = 0.20m;
            var expected = 1;

            //Act
            _target.AddScooter(id, pricePerMinute);
            var actual = _target.GetScooters().Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RemoveScooter_1_020_ScooterRemoved()
        {
            //Arrange
            var id = "1";
            var pricePerMinute = 0.20m;
            _target.AddScooter(id, pricePerMinute);

            //Act
            _target.RemoveScooter(id);

            //Assert
            Assert.Throws<ScooterNotFoundException>(() => _target.GetScooterById(id));
        }

        [Test]
        public void AddScooter_1_NegativePrice_ShouldFail()
        {
            //Arrange
            var id = "1";
            var pricePerMinute = -0.20m;

            //Act
            Assert.Throws<InvalidPriceException>(() => _target.AddScooter(id, pricePerMinute));
        }

        [Test]
        public void AddScooter_EmptyStringId_ShouldFail()
        {
            //Arrange
            var id = string.Empty;
            var pricePerMinute = 0.20m;

            //Act
            Assert.Throws<InvalidIdException>(() => _target.AddScooter(id, pricePerMinute));
        }

        [Test]
        public void AddScooter_WithExistingId_ShouldFail()
        {
            //Arrange
            var id = "1";
            var pricePerMinute = 0.20m;
            _target.AddScooter(id, pricePerMinute);

            //Act
            Assert.Throws<IdAlreadyUsedException>(() => _target.AddScooter(id, pricePerMinute));
        }

        [Test]
        public void AddScooter_NullId_ShouldFail()
        {
            //Arrange
            string id = null;
            var pricePerMinute = 0.20m;

            //Act
            Assert.Throws<InvalidIdException>(() => _target.AddScooter(id, pricePerMinute));
        }

        [Test]
        public void RemoveScooter_NotExisting_ShouldFail()
        {
            //Arrange
            var id = "1";

            //Assert
            Assert.Throws<ScooterNotFoundException>(() => _target.RemoveScooter(id));
        }

        [Test]
        public void GetScooters_ChangeInventoryWithoutService_ShouldFail()
        {
            //Arrange
            var scooters = _target.GetScooters();
            scooters.Add(new Scooter("1", 0.20m));
            scooters.Add(new Scooter("2", 0.20m));
            scooters.Add(new Scooter("3", 0.20m));

            //Assert
            Assert.AreEqual(0, _target.GetScooters().Count);
        }

    }
}