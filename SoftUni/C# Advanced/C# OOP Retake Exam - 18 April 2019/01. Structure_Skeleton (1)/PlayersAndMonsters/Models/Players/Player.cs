using PlayersAndMonsters.Models.Players.Contracts;
using PlayersAndMonsters.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Models.Players
{
    public abstract class Player : IPlayer
    {
        public Player(ICardRepository cardRepository, string username, int health)
        {
            this.cardRepository = cardRepository;
            this.Username = username;
            this.Health = health;
        }

        private ICardRepository cardRepository;
        public ICardRepository CardRepository => cardRepository;

        private string username;
        public string Username 
        {
            get => this.username;
            private set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Player's username cannot be null or an empty string.");
                }

                this.username = value;
            }
        }

        private int health;

        public int Health 
        {
            get => this.health;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Player's health bonus cannot be less than zero.");
                }

                this.health = value;
            }
        }

        public bool IsDead => this.Health <= 0;

        public void TakeDamage(int damagePoints)
        {
            if (damagePoints < 0)
            {
                throw new ArgumentException("Damage points cannot be less than zero.");
            }

            this.Health = Math.Max(this.Health - damagePoints, 0);
        }
    }
}
