using System;
using System.Collections.Generic;
using System.Text;

namespace _05._Football_Team_Generator
{
    public class Player
    {
        public Player(string name, PlayerStats.Stats stats)
        {
            this.Name = name;
            this.Stats = stats;
        }

        private string name;

        public string Name
        {
            get
            {
                return name; 
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("A name should not be empty.");
                }
                name = value;
            }
        }


        public PlayerStats.Stats Stats { get; set; }

        public int CalculatesTotalSkillLevel()
        {
            return this.Stats.Endurance.Stat
                + this.Stats.Dribble.Stat 
                + this.Stats.Passing.Stat
                + this.Stats.Sprint.Stat 
                + this.Stats.Shooting.Stat;
        }
    }
}
