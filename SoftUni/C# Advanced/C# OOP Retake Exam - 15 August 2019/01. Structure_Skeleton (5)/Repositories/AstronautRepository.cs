using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        public AstronautRepository()
        {
            this.models = new List<IAstronaut>();
        }


        private List<IAstronaut> models;
        public IReadOnlyCollection<IAstronaut> Models
        {
            get => this.models;

            private set
            {
                this.models = value.ToList();
            }
        }

        public void Add(IAstronaut model)
        {
            models.Add(model);
        }

        public IAstronaut FindByName(string name)
        {
            IAstronaut astronaut = models.Find(x => x.Name == name);

            if (astronaut == null)
            {
                return null;
            }

            return astronaut;
        }

        public bool Remove(IAstronaut model)
        {
            return models.Remove(model);
        }
    }
}
