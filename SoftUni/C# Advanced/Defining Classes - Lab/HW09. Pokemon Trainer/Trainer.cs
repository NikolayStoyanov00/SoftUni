using System;
using System.Collections.Generic;
using System.Text;

namespace HW09._Pokemon_Trainer
{
    public class Trainer
    {
        public string Name { get; set; }

        public int NumberOfBadges { get; set; }

        public List<Pokemon> Pokemons { get; set; }

        public Trainer (string name, int numberOfBadges, List<Pokemon> pokemons)
        {
            this.Name = name;
            this.NumberOfBadges = numberOfBadges;
            this.Pokemons = pokemons;
        }
    }
}
