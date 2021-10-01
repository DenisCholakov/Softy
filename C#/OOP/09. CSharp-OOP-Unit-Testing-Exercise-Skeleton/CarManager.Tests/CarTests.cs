using NUnit.Framework;
using System;

using CarManager;

namespace Tests
{
    public class CarTests
    {
        private Car car;
        [SetUp]
        public void Setup()
        {
            this.car = new Car("make", "model", 5, 100);
        }

        [Test]
        public void CarConstructorShouldSetAllDataCorrectly()
        {
            Assert.AreEqual("make", this.car.Make);
            Assert.AreEqual("model", this.car.Model);
            Assert.AreEqual(5, this.car.FuelConsumption);
            Assert.AreEqual(100, this.car.FuelCapacity);
        }

        [Test]
        public void CarConstructorShouldSetCarFuelAmountToZero()
        {
            Assert.AreEqual(0, this.car.FuelAmount);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void CarConstuctorShouldThrowExceptionIfMakeIsNullOrEmptyString(string make)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, "model", 5, 100));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void CarConstuctorShouldThrowExceptionIfModelIsNullOrEmptyString(string model)
        {
            Assert.Throws<ArgumentException>(() => new Car("make", model, 5, 100));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void CarConstructorShouldThrowExceptionIfFuelConsumptionIsZeroOrNegative(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>(() => new Car("make", "model", fuelConsumption, 100));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void CarConstructorShouldThrowExceptionIfFuelCapacityIsZeroOrNegative(double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() => new Car("make", "model", 5, fuelCapacity));
        }

        [Test]
        public void CarConstructorShouldSetFuelAmountToZero()
        {
            Assert.AreEqual(0, this.car.FuelAmount);
        }

        [Test]
        public void RefuelShouldAddFuelToTheFuelAmount()
        {
            double refuelAmount = 50;

            this.car.Refuel(refuelAmount);

            Assert.AreEqual(refuelAmount, this.car.FuelAmount);
        }

        [Test]
        public void RefuelShouldAddTheFuelCapacityIfTheAmountIsBigger()
        {
            double refuelAmount = 150;
            this.car.Refuel(refuelAmount);
            Assert.AreEqual(100, this.car.FuelAmount);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void RefuelShouldThrowExceptionIfRefuelAmountIsZeroOrNegative(double refuelAmount)
        {
            Assert.Throws<ArgumentException>(() => this.car.Refuel(refuelAmount));
        }

        [Test]
        public void DriveShouldReduceTheFuelAmountCorrectly()
        {
            this.car.Refuel(50);
            this.car.Drive(100);
            Assert.AreEqual(45, this.car.FuelAmount);
        }

        [Test]
        public void DriveShouldThroExceptionIfNeededFuelIsMoreThanTheFuelAmount()
        {
            Assert.Throws<InvalidOperationException>(() => this.car.Drive(40));
        }
    }
}