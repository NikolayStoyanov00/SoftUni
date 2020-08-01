using System;
using System.Collections.Generic;
using System.Text;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Models.Players.Contracts;
using ViceCity.Repositories;
using ViceCity.Repositories.Contracts;

namespace ViceCity.Models.Players
{
    public abstract class Player : IPlayer
    {
        protected Player(string name, int lifePoints)
        {
            this.Name = name;
            this.LifePoints = lifePoints;
            this.gunRepository = new GunRepository();
        }

        private string name;
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Player's name cannot be null or a whitespace!");
                }

                this.name = value;
            }
        }

        public bool IsAlive => this.LifePoints > 0;

        private IRepository<IGun> gunRepository;
        public IRepository<IGun> GunRepository
        {
            get => this.gunRepository;

            private set
            {
                this.gunRepository = value;
            }
        }

        private int lifePoints;
        public int LifePoints
        {
            get => this.lifePoints;

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Player life points cannot be below zero!");
                }

                this.lifePoints = value;
            }
        }

        public void TakeLifePoints(int points)
        {
            this.LifePoints -= points;

            if (this.LifePoints < 0)
            {
                this.LifePoints = 0;
            }
        }
    }
}
