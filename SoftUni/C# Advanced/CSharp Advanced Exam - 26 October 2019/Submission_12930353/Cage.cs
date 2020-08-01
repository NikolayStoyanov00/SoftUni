using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rabbits
{
    public class Cage
    {
        private List<Rabbit> data;

        public string Name { get; set; }

        public int Capacity { get; set; }

        public int Count => this.data.Count;

        public Cage(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;

            data = new List<Rabbit>();
        }

        public void Add(Rabbit rabbit)
        {
            if (data.Count <= Capacity)
            {
                data.Add(rabbit);
            }
        }

        public bool RemoveRabbit(string name)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Name == name)
                {
                    data.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void RemoveSpecies(string species)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Species == species)
                {
                    data.RemoveAt(i);
                }
            }
        }

        public Rabbit SellRabbit(string name)
        {
            for (int rabbitIndex = 0; rabbitIndex < data.Count; rabbitIndex++)
            {
                if (data[rabbitIndex].Name == name)
                {
                    data[rabbitIndex].Available = false;
                    return data[rabbitIndex];
                }
            }
            return null;
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
