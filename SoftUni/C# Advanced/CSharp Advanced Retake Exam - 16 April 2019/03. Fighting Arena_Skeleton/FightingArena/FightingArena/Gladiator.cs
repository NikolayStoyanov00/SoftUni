using System;
using System.Collections.Generic;
using System.Text;

namespace FightingArena
{
    public class Gladiator
    {
        public string Name { get; set; }

        public Stat Stat { get; set; }

        public Weapon Weapon { get; set; }

        public int GetTotalPower()
        {
            int totalPower = Stat.Strength + Stat.Skills + Stat.Flexibility + Stat.Intelligence + Stat.Agility
                + Weapon.Sharpness + Weapon.Size + Weapon.Solidity;

            return totalPower;
        }

        public int GetWeaponPower()
        {
            return Weapon.Sharpness + Weapon.Size + Weapon.Solidity;
        }

        public int GetStatPower()
        {
            return Stat.Strength + Stat.Skills + Stat.Flexibility + Stat.Intelligence + Stat.Agility;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.Name} - {GetTotalPower()}");
            sb.AppendLine($"  Weapon Power: {GetWeaponPower()}");
            sb.AppendLine($"  Stat Power: {GetStatPower()}");
            return sb.ToString();
        }

        public Gladiator(string name, Stat stat, Weapon weapon)
        {
            this.Stat = stat;
            this.Weapon = weapon;
            this.Name = name;
            ToString();
        }
    }
}
