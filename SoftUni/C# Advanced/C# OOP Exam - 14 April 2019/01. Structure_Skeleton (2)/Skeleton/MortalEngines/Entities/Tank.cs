using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities
{
    public class Tank : BaseMachine, ITank
    {
        private const double initialAttack = 40;
        private const double initialDefense = 30;
        public Tank(string name, double attackPoints, double defensePoints) 
            : base(name, attackPoints - initialAttack, defensePoints + initialDefense, healthPoints)
        {
            this.DefenseMode = true;   
        }
        private const double healthPoints = 100;


        public bool DefenseMode { get; private set; }

        public void ToggleDefenseMode()
        {
            if (DefenseMode == true)
            {
                DefenseMode = false;
                this.AttackPoints += 40;
                this.DefensePoints -= 30;
            }
            else
            {
                DefenseMode = true;
                this.AttackPoints -= 40;
                this.DefensePoints += 30;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"- {this.Name}");
            sb.AppendLine($" *Type: {this.GetType().Name}");
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

            if (this.DefenseMode == true)
            {
                sb.AppendLine($" *Defense: ON");
            }
            else
            {
                sb.AppendLine(" *Defense: OFF");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
