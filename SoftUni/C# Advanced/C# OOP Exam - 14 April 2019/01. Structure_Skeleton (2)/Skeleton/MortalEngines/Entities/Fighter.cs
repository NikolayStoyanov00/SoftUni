using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities
{
    public class Fighter : BaseMachine, IFighter
    {
        private const double healthPoints = 200;
        private const double initialAttack = 50;
        private const double initialDefense = 25;
        public Fighter(string name, double attackPoints, double defensePoints)
            : base(name, attackPoints + initialAttack, defensePoints - initialDefense, healthPoints)
        {
            this.AggressiveMode = true;
        }

        public bool AggressiveMode { get; private set; }

        public void ToggleAggressiveMode()
        {
            if (this.AggressiveMode == true)
            {
                this.AggressiveMode = false;
                this.AttackPoints -= initialAttack;
                this.DefensePoints += initialDefense;
            }
            else
            {
                this.AggressiveMode = true;
                this.AttackPoints += initialAttack;
                this.DefensePoints -= initialDefense;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"- {this.Name}");
            sb.AppendLine($" *Type: {this.GetType().Name:F2}");
            sb.AppendLine($" *Health: {this.HealthPoints:F2}");
            sb.AppendLine($" *Attack: {this.AttackPoints:F2}");
            sb.AppendLine($" *Defense: {this.DefensePoints:F2}");
            sb.Append($" *Targets: ");

            if (Targets.Count > 0)
            {
                sb.AppendLine(string.Join(",", Targets));
            }
            else
            {
                sb.AppendLine("None");
            }

            if (this.AggressiveMode == true)
            {
                sb.AppendLine($" *Aggressive: ON");
            }
            else
            {
                sb.AppendLine(" *Aggressive: OFF");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
