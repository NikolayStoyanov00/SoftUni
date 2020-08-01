using System;

namespace DefiningClasses
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Person firstPerson = new Person();
            firstPerson.Name = "Pesho";
            firstPerson.Age = 20;

            Person secondPerson = new Person();
            secondPerson.Name = "Gosho";
            secondPerson.Age = 18;

            Person thirdPerson = new Person();
            thirdPerson.Name = "Stamat";
            thirdPerson.Age = 43;
        }
    }
}
