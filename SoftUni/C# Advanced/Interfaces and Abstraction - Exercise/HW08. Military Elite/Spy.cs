using System;
using System.Collections.Generic;
using System.Text;

namespace _08._Military_Elite
{
    public class Spy
    {
        private int id;
        private string firstName;
        private string lastName;

        public Spy(int id, string firstName, string lastName, int codeNumber)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            CodeNumber = codeNumber;
        }

        public int CodeNumber { get; set; }
    }
}
