using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        public Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;

            this.decorations = new List<IDecoration>();
            this.fish = new List<IFish>();
        }

        private string name;
        public string Name
        {
            get => this.name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Aquarium name cannot be null or empty."); 
                }

                this.name = value;
            }
        }

        private int capacity;
        public int Capacity
        {
            get => this.capacity;
            //Check here if structure not 50/50.
            private set
            {
                this.capacity = value;
            }
        }

        public int Comfort => decorations.Sum(x => x.Comfort);
        

        private List<IDecoration> decorations;
        // TODO: initialize the list.
        public ICollection<IDecoration> Decorations
        {
            get => this.decorations;

            private set
            {
                this.decorations = value.ToList();
            }
        }

        private List<IFish> fish;
        //TODO: initialize the list.
        public ICollection<IFish> Fish
        {
            get => this.fish;

            private set
            {
                this.fish = value.ToList();
            }
        }

        public void AddDecoration(IDecoration decoration)
        {
            decorations.Add(decoration);
        }

        public void AddFish(IFish fish)
        {
            if (this.fish.Sum(x => x.Size) + fish.Size <= this.Capacity)
            {
                this.fish.Add(fish);
            }
            else
            {
                throw new InvalidOperationException("Not enough capacity.");
            }
        }

        public void Feed()
        {
            this.fish.ForEach(x => x.Eat());
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.Name} ({this.GetType().Name})");

            sb.Append("Fish: ");
            if (this.fish.Count > 0)
            {
                for (int i = 0; i < this.fish.Count; i++)
                {
                    if (i < this.fish.Count - 1)
                    {
                        sb.Append($"{fish[i].Name}, ");
                    }
                    else
                    {
                        sb.AppendLine(fish[i].Name);
                    }
                }

            }
            else if (this.fish.Count == 0)
            {
                sb.AppendLine("none");
            }

            sb.AppendLine($"Decorations: {this.Decorations.Count}");
            sb.AppendLine($"Comfort: {this.Comfort}");

            return sb.ToString().TrimEnd();
        }

        public bool RemoveFish(IFish fish)
        {
            return this.fish.Remove(fish);
        }
    }
}
