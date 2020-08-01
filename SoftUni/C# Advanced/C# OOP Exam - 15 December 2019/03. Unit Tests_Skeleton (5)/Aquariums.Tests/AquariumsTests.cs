namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;

    public class AquariumsTests
    {
        [Test]
        public void FishConstructorShouldInitializeCorrectly()
        {
            Fish fish = new Fish("Miki");

            Assert.AreEqual(fish.Name, "Miki");
            Assert.IsTrue(fish.Available == true);
        }

        [Test]
        public void AquariumConstructorShouldWorkCorrectly()
        {
            Aquarium aquarium = new Aquarium("Pesho", 20);

            Assert.AreEqual(aquarium.Name, "Pesho");
            Assert.AreEqual(aquarium.Capacity, 20);
            Assert.IsTrue(aquarium.Count == 0);
        }

        [Test]
        public void NameShouldReturnCorrectValue()
        {
            Aquarium aquarium = new Aquarium("Pesho", 20);

            Assert.AreEqual(aquarium.Name, "Pesho");
        }

        [Test]
        public void NameShouldThrowArgumentNullExceptionWhenInvalid()
        {
            Assert.Throws<ArgumentNullException>(() => new Aquarium(null, 20));
        }
        [Test]
        public void NameShouldThrowArgumentNullExceptionWhenEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => new Aquarium(string.Empty, 20));
        }

        [Test]
        public void CapacityShouldThrowArgumentExceptionWhenBelowZero()
        {
            Assert.Throws<ArgumentException>(() => new Aquarium("Pesho", -2));
        }

        [Test]
        public void CapacityShouldReturnCorrectValue()
        {
            Aquarium aquarium = new Aquarium("Pesho", 5);

            Assert.AreEqual(aquarium.Capacity, 5);
        }

        [Test]
        public void CountShouldReturnCorrectValues()
        {
            Aquarium aquarium = new Aquarium("Misho", 400);

            aquarium.Add(new Fish("Nemo"));
            aquarium.Add(new Fish("Dori"));

            Assert.AreEqual(aquarium.Count, 2);
        }

        [Test]
        public void AddMethodShouldAddFishToAquarium()
        {
            Aquarium aquarium = new Aquarium("Misho", 400);
            aquarium.Add(new Fish("Nemo"));

            Assert.IsTrue(aquarium.Count == 1);
        }

        [Test]
        public void CapacityShouldThrowInvalidOperationExceptionWhenCountIsEqual()
        {
            Aquarium aquarium = new Aquarium("Misho", 2);

            aquarium.Add(new Fish("Nemo"));
            aquarium.Add(new Fish("Dori"));

            Assert.Throws<InvalidOperationException>(() => aquarium.Add(new Fish("Zdr")));
        }

        [Test]
        public void RemoveFishShouldThrowInvalidOperationExceptionWhenFishDoesntExist()
        {
            Aquarium aquarium = new Aquarium("Misho", 2);
            aquarium.Add(new Fish("Nemo"));

            Assert.Throws<InvalidOperationException>(() => aquarium.RemoveFish("Reksi"));
        }

        [Test]
        public void RemoveFishShouldRemoveTheFishFromAquarium()
        {
            Aquarium aquarium = new Aquarium("Mish", 2);
            aquarium.Add(new Fish("Nemo"));

            aquarium.RemoveFish("Nemo");

            Assert.AreEqual(aquarium.Count, 0);
        }

        [Test]
        public void SellFishShouldThrowInvalidOperationExceptionWhenRequestedFishNull()
        {
            Aquarium aquarium = new Aquarium("Misho", 2);

            Assert.Throws<InvalidOperationException>(() => aquarium.SellFish("Mincho"));
        }

        [Test]
        public void SellFishShouldSellTheFishCorrectly()
        {
            Aquarium aquarium = new Aquarium("Misho", 5);
            Fish fish = new Fish("Nemo");
            aquarium.Add(fish);
            aquarium.SellFish("Nemo");

            Assert.IsFalse(fish.Available);
        }

        [Test]
        public void ReportShouldReturnCorrectValue()
        {
            Aquarium aquarium = new Aquarium("Misho", 2);

            aquarium.Add(new Fish("Nemo"));
            aquarium.Add(new Fish("Dori"));

            string report = aquarium.Report();

            Assert.AreEqual(report, "Fish available at Misho: Nemo, Dori");
        }

        [Test]
        public void ReportShouldReturnCorrectValueAgain()
        {
            Aquarium aquarium = new Aquarium("Misho", 2);

            aquarium.Add(new Fish("Nemo"));

            string report = aquarium.Report();

            Assert.AreEqual(report, "Fish available at Misho: Nemo");
        }
    }
}
