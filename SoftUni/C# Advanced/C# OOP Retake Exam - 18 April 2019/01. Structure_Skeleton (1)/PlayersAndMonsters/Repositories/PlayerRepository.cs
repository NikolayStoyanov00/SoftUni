using PlayersAndMonsters.Models.Players.Contracts;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        public int Count => players.Count;

        public PlayerRepository()
        {
            players = new Dictionary<string, IPlayer>();
        }

        private Dictionary<string, IPlayer> players;
        public IReadOnlyCollection<IPlayer> Players => players.Values;

        public void Add(IPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentException("Player cannot be null");
            }

            if (players.ContainsKey(player.Username))
            {
                throw new ArgumentException($"Player {player.Username} already exists!");
            }

            players.Add(player.Username, player);
        }

        public IPlayer Find(string username)
        {
            return players[username];
        }

        public bool Remove(IPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentException("Player cannot be null");
            }

            if (players.Remove(player.Username))
            {
                return true;
            }

            return false;
        }
    }
}
