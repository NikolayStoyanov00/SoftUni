using SpaceStation.Models.Planets;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        public PlanetRepository()
        {
            this.models = new List<IPlanet>();
        }

        private List<IPlanet> models;
        public IReadOnlyCollection<IPlanet> Models
        {
            get => this.models;

            private set
            {
                this.models = value.ToList();
            }
        }

        public void Add(IPlanet model)
        {
            models.Add(model);
        }

        public IPlanet FindByName(string name)
        {
            return models.Find(x => x.Name == name);
        }

        public bool Remove(IPlanet model)
        {
            return models.Remove(model);
        }
    }
}
