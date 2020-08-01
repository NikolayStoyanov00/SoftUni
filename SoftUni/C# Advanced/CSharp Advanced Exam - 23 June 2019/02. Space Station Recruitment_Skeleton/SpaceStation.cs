using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStationRecruitment
{
    public class SpaceStation
    {
        private List<Astronaut> data;
        public string Name { get; set; }

        public int Capacity { get; set; }

        public int Count => data.Count;
        public SpaceStation(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            data = new List<Astronaut>();
        }

        public void Add(Astronaut astronaut)
        {
            if (data.Count < Capacity)
            {
                data.Add(astronaut);
            }
        }

        public bool Remove(string name)
        {
            foreach (var astronaut in data)
            {
                if (astronaut.Name == name)
                {
                    data.Remove(astronaut);
                    return true;
                }
            }
            return false;
        }
        public Astronaut GetOldestAstronaut()
        {
            int oldestAge = 0;
            Astronaut biggestAstronaut = new Astronaut("", 0, "");

            foreach (var astronaut in data)
            {
                if (astronaut.Age > oldestAge)
                {
                    biggestAstronaut = astronaut;
                    oldestAge = astronaut.Age;
                }
            }
            return biggestAstronaut;
        }

        public Astronaut GetAstronaut(string name)
        {
            foreach (var astronaut in data)
            {
                if (astronaut.Name == name)
                {
                    return astronaut;
                }
            }
            return null;
        }

        public string Report()
        {
            string report = $"Astronauts working at Space Station {Name}:" +
                $"{Environment.NewLine}" +
                $"{string.Join(Environment.NewLine, data)}";

            return report;
        }
    }
}
