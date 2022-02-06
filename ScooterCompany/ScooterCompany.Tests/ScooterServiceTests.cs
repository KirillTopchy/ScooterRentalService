 using NUnit.Framework;
 using ScooterCompany.Exceptions;
 using ScooterCompany.Interfaces;
 using ScooterCompany.Models;

 namespace ScooterCompany.Tests
{
    public class ScooterServiceTests
    {
        private IScooterService _target;
        private const string Id = "1";
        private const decimal PricePerMinute = 0.20m;


        [SetUp]
        public void Setup()
        {
            _target = new ScooterService();
        }

        [Test]
        public void AddScooter_1_020_ScooterAdded()
        {
            //Arrange 
            var expected = 1;

            //Act
            _target.AddScooter(Id, PricePerMinute);
            var actual = _target.GetScooters().Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RemoveScooter_1_020_ScooterRemoved()
        {
            //Arrange
            _target.AddScooter(Id, PricePerMinute);

            //Act
            _target.RemoveScooter(Id);

            //Assert
            Assert.Throws<ScooterNotFoundException>(() => _target.GetScooterById(Id));
        }

        [Test]
        public void AddScooter_1_NegativePrice_ShouldFail()
        {
            //Arrange
            var pricePerMinute = -0.20m;

            //Act
            Assert.Throws<InvalidPriceException>(() => _target.AddScooter(Id, pricePerMinute));
        }

        [Test]
        public void AddScooter_EmptyStringId_ShouldFail()
        {
            //Arrange
            var id = string.Empty;

            //Act
            Assert.Throws<InvalidIdException>(() => _target.AddScooter(id, PricePerMinute));
        }

        [Test]
        public void AddScooter_WithExistingId_ShouldFail()
        {
            //Arrange
            _target.AddScooter(Id, PricePerMinute);

            //Act
            Assert.Throws<IdAlreadyUsedException>(() => _target.AddScooter(Id, PricePerMinute));
        }

        [Test]
        public void AddScooter_1_GetSameScooterBack()
        {
            //Arrange
            _target.AddScooter(Id, PricePerMinute);

            //Act
            var scooter = _target.GetScooterById(Id);

            //Assert
            Assert.AreEqual(Id, scooter.Id);
        }

        [Test]
        public void AddScooter_NullId_ShouldFail()
        {
            //Arrange
            string id = null;

            //Act
            Assert.Throws<InvalidIdException>(() => _target.AddScooter(id, PricePerMinute));
        }

        [Test]
        public void RemoveScooter_NotExisting_ShouldFail()
        {
            //Assert
            Assert.Throws<ScooterNotFoundException>(() => _target.RemoveScooter(Id));
        }

        [Test]
        public void GetScooters_ChangeInventoryWithoutService_ShouldFail()
        {
            //Arrange
            var scooters = _target.GetScooters();
            scooters.Add(new Scooter(Id, PricePerMinute));
            scooters.Add(new Scooter("2", PricePerMinute));
            scooters.Add(new Scooter("3", PricePerMinute));

            //Assert
            Assert.AreEqual(0, _target.GetScooters().Count);
        }
    }
}