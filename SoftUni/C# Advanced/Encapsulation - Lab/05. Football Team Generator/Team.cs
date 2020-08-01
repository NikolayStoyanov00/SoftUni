using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _05._Football_Team_Generator
{
    public class Team
    {
        public string Name { get; set; }
        public int Rating
        {
            get { return CalculateTeamRating(); }
        }

        private List<Player> players;

        public Team(string name)
        {
            this.Name = name;
            players = new List<Player>();
        }

        private int CalculateTeamRating()
        {
            if (this.players.Any())
            {
                return (int)Math.Round(this.players.Average(p => p.Stats.TotalStats));
            }
            else
            {
                return 0;
            }
        }
        public void AddPlayer(Player player)
        {
            this.players.Add(player);
        }

        public void RemovePlayer(string playerName)
        {
            Player player = players.FirstOrDefault(x => x.Name == playerName);

            if (player == null)
            {
                throw new Exception($"Player {playerName} is not in {this.Name} team.");
            }

            players.Remove(player);
        }
    }
}
