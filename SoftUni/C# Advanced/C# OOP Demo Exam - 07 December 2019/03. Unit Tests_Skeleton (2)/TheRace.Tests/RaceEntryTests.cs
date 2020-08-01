using NUnit.Framework;
using System;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CalculateAverageHorsePowerShouldReturnCorrectValues()
        {
            RaceEntry raceEntry = new RaceEntry();

            UnitMotorcycle unitMotorcycle = new UnitMotorcycle("NZ", 123, 100);
            UnitRider unitRider = new UnitRider("Pesho", unitMotorcycle);
            UnitRider secondUnitRider = new UnitRider("Misho", unitMotorcycle);

            raceEntry.AddRider(unitRider);
            raceEntry.AddRider(secondUnitRider);

            double result = raceEntry.CalculateAverageHorsePower();
            Assert.IsTrue(result == 123);
        }

        [Test]
        public void CounterShouldReturnCorrectValue()
        {
            UnitMotorcycle unitMotorcycle = new UnitMotorcycle("NZ", 123, 100);
            UnitRider unitRider = new UnitRider("Pesho", unitMotorcycle);

            RaceEntry raceEntry = new RaceEntry();
            raceEntry.AddRider(unitRider);

            Assert.IsTrue(raceEntry.Counter == 1);
        }

        [Test]
        public void AddRiderShouldThrowInvalidOperationExceptionIfRiderEqualsNull()
        {
            RaceEntry raceEntry = new RaceEntry();
            UnitRider unitRider = null;
            
            Assert.Throws<InvalidOperationException>(() => raceEntry.AddRider(unitRider));
        }

        [Test]
        public void AddRiderShouldThrowInvalidOperationExceptionIfRiderExists()
        {
            UnitMotorcycle unitMotorcycle = new UnitMotorcycle("NZ", 123, 123.2);
            UnitRider unitRider = new UnitRider("Pesho", unitMotorcycle);
            UnitRider secondUnitRider = new UnitRider("Pesho", unitMotorcycle);

            RaceEntry raceEntry = new RaceEntry();
            raceEntry.AddRider(unitRider);
            Assert.Throws<InvalidOperationException>(() => raceEntry.AddRider(secondUnitRider));
        }

        [Test]
        public void CalculateAverageHorsePowerShouldThrowInvalidOperationExceptionWhenCountLessThanMinParticipants()
        {
            UnitMotorcycle unitMotorcycle = new UnitMotorcycle("NZ", 123, 123.2);
            UnitRider unitRider = new UnitRider("Pesho", unitMotorcycle);

            RaceEntry raceEntry = new RaceEntry();
            raceEntry.AddRider(unitRider);

            Assert.Throws<InvalidOperationException>(() => raceEntry.CalculateAverageHorsePower());
        }
    }
}