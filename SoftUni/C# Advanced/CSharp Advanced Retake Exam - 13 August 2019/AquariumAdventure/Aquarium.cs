using System;
using System.Collections.Generic;
using System.Text;

namespace AquariumAdventure
{
    public class Aquarium
    {
        private List<Fish> fishInPool;
        public string Name { get; set; }

        public int Capacity { get; set; }

        public int Size { get; set; }

        
        public Aquarium(string name, int capacity, int size)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.Size = size;
            fishInPool = new List<Fish>();
        }

        public void Add(Fish fish)
        {
            foreach (var currentFish in fishInPool)
            {
                if (currentFish.Name == fish.Name)
                {
                    return;
                }
                if (fishInPool.Count > Capacity)
                {
                    return;
                }
            }
            fishInPool.Add(fish);
        }

        public string Remove(string name)
        {
            foreach (var currentFish in fishInPool)
            {
                if (currentFish.Name == name)
                {
                    fishInPool.Remove(currentFish);
                    return "true";
                }
            }
            return "false";
        }

        public string FindFish(string name)
        {
            foreach (var currentFish in fishInPool)
            {
                if (currentFish.Name == name)
                {
                    return currentFish.ToString().TrimEnd();
                }
            }
            return null;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Aquarium: {Name} ^ Size: {Size}");
            foreach (var currentFish in fishInPool)
            {
                sb.AppendLine(currentFish.ToString());
            }
            
            return sb.ToString().TrimEnd();
        }
    }
}
