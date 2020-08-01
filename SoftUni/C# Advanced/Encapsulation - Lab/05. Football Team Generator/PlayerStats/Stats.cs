using System;
using System.Collections.Generic;
using System.Text;

namespace _05._Football_Team_Generator.PlayerStats
{
    public class Stats
    {
        private Endurance endurance;
        public Endurance Endurance { 
            get
            {
                return endurance;
            }
            set
            {
                if (value.Stat < 0 || value.Stat > 100)
                {
                    throw new Exception($"{nameof(Endurance)} should be between 0 and 100.");
                }
                this.endurance = value;
            }
        }

        public int TotalStats { get { return CalculateAverageStats(); } }

        private Sprint sprint;

        public Sprint Sprint
        {
            get
            { 
                return sprint;
            }
            set
            {
                if (value.Stat < 0 || value.Stat > 100)
                {
                    throw new Exception($"{nameof(Sprint)} should be between 0 and 100.");
                }

                this.sprint = value;
            }
        }

        private Dribble dribble;

        public Dribble Dribble
        {
            get 
            { 
                return dribble; 
            }
            set 
            {
                if (value.Stat < 0 || value.Stat > 100)
                {
                    throw new Exception($"{nameof(Dribble)} should be between 0 and 100.");
                }

                this.dribble = value; 
            }
        }

        private Passing passing;

        public Passing Passing
        {
            get
            {
                return this.passing;
            }
            set
            {
                if (value.Stat < 0 || value.Stat > 100)
                {
                    throw new Exception($"{nameof(Passing)} should be between 0 and 100.");
                }

                this.passing = value;
            }
        }

        private Shooting shooting;

        public Shooting Shooting
        {
            get
            {
                return this.shooting;
            }
            set
            {
                if (value.Stat < 0 || value.Stat > 100)
                {
                    throw new Exception($"{nameof(Shooting)} should be between 0 and 100.");
                }

                this.shooting = value;
            }
        }

        private int CalculateAverageStats()
        {
            return (int)Math.Round((this.Dribble.Stat + this.Endurance.Stat + this.Passing.Stat + this.Shooting.Stat + this.Sprint.Stat) / (double)5);
        }

        public Stats(int enduranceStat, int sprintStat, int dribbleStat, int passingStat, int shootingStat)
        {
            this.Endurance = new Endurance(enduranceStat);
            this.Sprint = new Sprint(sprintStat);
            this.Dribble = new Dribble(dribbleStat);
            this.Passing = new Passing(passingStat);
            this.Shooting = new Shooting(shootingStat);
        }
    }
}
