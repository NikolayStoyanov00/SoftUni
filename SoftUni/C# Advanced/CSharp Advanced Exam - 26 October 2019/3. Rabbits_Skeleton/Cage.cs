using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbits
{
    public class Cage
    {
        private List<Rabbit> data;

        public string Name { get; set; }
        public int Capacity { get; set; }

        public int Count => data.Count;
        public Cage (string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;

            data = new List<Rabbit>();
        }

        public void Add(Rabbit rabbit)
        {
            if (data.Count < Capacity)
            {
                data.Add(rabbit);
            }
        }

        public bool RemoveRabbit(string name)
        {
            foreach (Rabbit rabbit in data)
            {
                if (rabbit.Name == name)
                {
                    data.Remove(rabbit);
                    return true;
                }
            }
            return false;
        }

        public void RemoveSpecies(string species)
        {
            foreach (Rabbit rabbit in data)
            {
                if (rabbit.Species == species)
                {
                    data.Remove(rabbit);
                }
            }
        }

        public Rabbit SellRabbit(string name)
        {
            Rabbit returnRabbit = data[0];

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Name == name)
                {
                    data[i].Available = false;
                    returnRabbit = data[i];
                    break;
                }
            }

            return returnRabbit;
        }

        public Rabbit[] SellRabbitsBySpecies(string species)
        {
            List<Rabbit> rabbits = new List<Rabbit>();

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Species == species)
                {
                    data[i].Available = false;
                    rabbits.Add(data[i]);
                }
            }

            return rabbits.ToArray();
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Rabbits available at {Name}:");

            foreach (Rabbit rabbit in data)
            {
                if (rabbit.Available == true)
                {
                    sb.AppendLine(rabbit.ToString());
                }
            }
            return sb.ToString().TrimEnd();
        }
    }
}
