using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities
{
    public abstract class BaseMachine : IMachine
    {
        protected BaseMachine(string name, double attackPoints, double defensePoints, double healthPoints)
        {
            this.Name = name;
            this.AttackPoints = attackPoints;
            this.DefensePoints = defensePoints;
            this.HealthPoints = healthPoints;
        }

        private string name;
        public string Name
        {
            get => this.name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Machine name cannot be null or empty");
                }

                this.name = value;
            }
        }

        private IPilot pilot;
        public IPilot Pilot
        {
            get => this.pilot;
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("Pilot cannot be null.");
                }

                this.pilot = value;
            }
        }

        private double healthPoints;
        public double HealthPoints
        {
            get => this.healthPoints;
            set => this.healthPoints = value;
        }

        private double attackPoints;

        public double AttackPoints
        {
            get => this.attackPoints;

            protected set => this.attackPoints = value;
        }

        private double defensePoints;
        public double DefensePoints
        {
            get => this.defensePoints;

            protected set => this.defensePoints = value;
        }

        private IList<string> targets = new List<string>();

        public IList<string> Targets
        {
            get => this.targets;

            private set => this.targets = value;
        }

        public void Attack(IMachine target)
        {
            if (target == null)
            {
                throw new NullReferenceException("Target cannot be null");
            }

            target.HealthPoints -= (this.AttackPoints - target.DefensePoints);

            if (target.HealthPoints < 0)
            {
                target.HealthPoints = 0;
            }

            targets.Add(target.Name);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"- {this.Name}");
            sb.AppendLine($" *Type: {this.GetType().Name}");
            sb.AppendLine($" *Health: {this.HealthPoints}");
            sb.AppendLine($" *Attack: {this.AttackPoints}");
            sb.AppendLine($" *Defense: {this.DefensePoints}");
            sb.Append($" *Targets: ");

            if (Targets.Count > 0)
            {
                sb.AppendLine(string.Join(",", Targets));
            }
            else
            {
                sb.AppendLine("None");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
