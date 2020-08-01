using _08._Military_Elite.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _08._Military_Elite.Models
{
    public class Commando
    {
        public Commando(int id, string firstName, string lastName, decimal salary, Corps corps)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
            Corps = corps;
            Missions = new HashSet<Mission>();
        }

        public int Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public decimal Salary { get; }

        public Corps Corps { get; }

        public ICollection<Mission> Missions { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Name: {this.FirstName} {this.LastName} Id: {this.Id} Salary: {this.Salary:F2}")
                .AppendLine($"Corps: {this.Corps}")
                .AppendLine("Missions:");
            foreach (var mission in this.Missions)
            {
                sb.AppendLine(mission.ToString());
            }

            return sb.ToString().Trim();
        }
}
}
