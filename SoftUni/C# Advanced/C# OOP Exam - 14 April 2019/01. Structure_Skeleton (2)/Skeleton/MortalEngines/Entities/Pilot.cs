using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities
{
    public class Pilot : IPilot
    {
        
        public Pilot(string name)
        {
            this.Name = name;
            machines = new List<IMachine>();
        }

        private string name;
        public string Name
        {
            get => this.name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Pilot name cannot be null or empty string.");
                }

                this.name = value;
            }
        }

        private List<IMachine> machines;
        public void AddMachine(IMachine machine)
        {
            if (machine == null)
            {
                throw new NullReferenceException("Null machine cannot be added to the pilot.");
            }

            machines.Add(machine);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.Name} - {this.machines.Count} machines");

            foreach (var machine in machines)
            {
                sb.AppendLine($"- {machine.Name}");
                sb.AppendLine($" *Type: {machine.GetType().Name}");
                sb.AppendLine($" *Health: {machine.HealthPoints:F2}");
                sb.AppendLine($" *Attack: {machine.AttackPoints:F2}");
                sb.AppendLine($" *Defense: {machine.DefensePoints:F2}");
                sb.AppendLine($" *Targets: {machine.Targets}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
