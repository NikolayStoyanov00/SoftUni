namespace BlueOrigin.Tests
{
    using System;
    using NUnit.Framework;

    public class SpaceshipTests
    {
        [Test]
        public void AstronautConstructorShouldWorkCorrectly()
        {
            Astronaut astronaut = new Astronaut("Pesho", 2.2);

            Assert.IsTrue(astronaut.Name == "Pesho");
            Assert.IsTrue(astronaut.OxygenInPercentage == 2.2);
        }

        [Test]
        public void SpaceshipConstructorShouldWorkCorrectly()
        {
            Spaceship spaceship = new Spaceship("Pesho", 5);

            Assert.IsTrue(spaceship.Name == "Pesho");
            Assert.IsTrue(spaceship.Capacity == 5);
            Assert.AreEqual(spaceship.Count, 0);
        }

        [Test]
        public void CountShouldReturnCorrectValue()
        {
            Spaceship spaceship = new Spaceship("Pesho", 5);
            Astronaut astronaut = new Astronaut("Misho", 2.2);
            spaceship.Add(astronaut);

            Assert.IsTrue(spaceship.Count == 1);
        }

        [Test]
        public void SpaceshipShouldThrowArgumentNullExceptionWhenNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Spaceship(null, 2));
        }

        [Test]
        public void SpaceshipShouldThrowArgumentWhenEmptyExceptionWhenNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Spaceship("", 2));
        }


        [Test]
        public void SpaceshipShouldThrowArgumentExceptionWhenCapacityIsLessThanZero()
        {
            Assert.Throws<ArgumentException>(() => new Spaceship("Misho", -2));
        }

        [Test]
        public void SpaceshipShouldThrowInvalidOperationExceptionWhenCapacityIsEqualToCount()
        {
            Spaceship spaceship = new Spaceship("Pesho", 1);
            Astronaut astronaut = new Astronaut("Misho", 2.2);
            Astronaut astronautTwo = new Astronaut("Pesh", 2.1);
            spaceship.Add(astronaut);
            Assert.Throws<InvalidOperationException>(() => spaceship.Add(astronautTwo));
        }

        [Test] 
        public void SpaceshipShouldThrowInvalidOperationExceptionWhenSameAstronautIsAdded()
        {
            Spaceship spaceship = new Spaceship("Pesho", 1);
            Astronaut astronaut = new Astronaut("Misho", 2.2);
            Astronaut astronautTwo = new Astronaut("Misho", 2.5);

            spaceship.Add(astronaut);

            Assert.Throws<InvalidOperationException>(() => spaceship.Add(astronautTwo));
        }

        [Test]
        public void Test_Remove_True()
        {
            Spaceship spaceship = new Spaceship("Misho", 2);

            Astronaut astronaut1 = new Astronaut("Name1", 2.2);
            Astronaut astronaut2 = new Astronaut("Name2", 23.2);
            spaceship.Add(astronaut1);
            spaceship.Add(astronaut2);
            Assert.AreEqual(true, spaceship.Remove("Name1"));
        }
        [Test]
        public void Test_Add()
        {
            Spaceship spaceship = new Spaceship("Pesho", 5);

            Astronaut astronaut = new Astronaut("Misho", 2.2);
            Astronaut astronautTwo = new Astronaut("Pesho", 2.2);
            Astronaut astronautThree = new Astronaut("Ri6ko", 2.2);

            spaceship.Add(astronaut);
            spaceship.Add(astronautTwo);
            spaceship.Add(astronautThree);

            Assert.AreEqual(spaceship.Count, 3);
        }

        [Test]
        public void SpaceshipShouldReturnFalseWhenAstronautIsNotFound()
        {
            Spaceship spaceship = new Spaceship("Pesho", 1);
            Astronaut astronaut = new Astronaut("Misho", 2.2);
            spaceship.Add(astronaut);
            bool removed = spaceship.Remove("Peshko");
            Assert.AreEqual(false, removed);
        }
    }
}