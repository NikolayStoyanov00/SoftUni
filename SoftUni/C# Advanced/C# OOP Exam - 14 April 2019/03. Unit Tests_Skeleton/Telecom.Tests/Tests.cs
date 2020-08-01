namespace Telecom.Tests
{
    using NUnit.Framework;
    using System;

    public class Tests
    {
        [Test]
        public void PhoneConstructorShouldWorkAsIntended()
        {
            Phone phone = new Phone("123", "Nokia");

            Assert.AreSame(phone.Make, "123");
            Assert.AreSame(phone.Model, "Nokia");
        }

        [Test]
        public void MakeShouldThrowArgumentExceptionWhenNull()
        {
            Assert.Throws<ArgumentException>(() => new Phone(null, "Nokia"));
        }

        [Test]
        public void ModelShouldThrowArgumentExceptionWhenNull()
        {
            Assert.Throws<ArgumentException>(() => new Phone("123", null));
        }

        [Test]
        public void CountShouldReturnCorrectValue()
        {
            Phone phone = new Phone("123", "Nokia");

            phone.AddContact("Pesho", "12345");

            Assert.IsTrue(phone.Count == 1);
        }

        [Test]
        public void AddContactShouldThrowInvalidOperationExceptionWhenPersonAlreadyExists()
        {
            Phone phone = new Phone("123", "Nokia");

            phone.AddContact("Pesho", "12345");

            Assert.Throws<InvalidOperationException>(() => phone.AddContact("Pesho", "12345"));
        }

        [Test]
        public void CallShouldThrowInvalidOperationExceptionWhenPersonDoesntExist()
        {
            Phone phone = new Phone("123", "Nokia");
            phone.AddContact("Eli", "12345");
            Assert.Throws<InvalidOperationException>(() => phone.Call("Pesho"));
        }

        [Test]
        public void CallReturnsCallingWhenValid()
        {
            Phone phone = new Phone("123", "Nokia");
            phone.AddContact("Eli", "12345");

            Assert.IsTrue(phone.Call("Eli") == $"Calling Eli - 12345...");
        }
    }
}