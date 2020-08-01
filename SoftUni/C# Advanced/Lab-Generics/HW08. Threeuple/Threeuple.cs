using System;
using System.Collections.Generic;
using System.Text;

namespace Threeuple
{
    public class Threeuple
    {
        public string Name { get; set; }

        public string Address { get; set; }
        public string Town { get; set; }

        public int BeerLitres { get; set; }

        public string DrunkOrNot { get; set; }

        public double BankBalance { get; set; }

        public string BankName { get; set; }

        public Threeuple(string name, string address, string town)
        {
            this.Name = name;
            this.Address = address;
            this.Town = town;
        }

        public Threeuple(string name, int beerLitres, string drunkOrNot)
        {
            this.Name = name;
            this.BeerLitres = beerLitres;
            this.DrunkOrNot = drunkOrNot;
        }

        public Threeuple(string name, double bankBalance, string bankName)
        {
            this.Name = name;
            this.BankBalance = bankBalance;
            this.BankName = bankName;
        }
    }
}