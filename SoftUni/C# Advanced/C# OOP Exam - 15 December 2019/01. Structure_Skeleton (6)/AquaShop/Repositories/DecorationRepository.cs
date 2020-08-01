using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Repositories
{
    public class DecorationRepository : IRepository<IDecoration>
    {
        public DecorationRepository()
        {
            this.models = new List<IDecoration>();
        }

        private List<IDecoration> models;
        public IReadOnlyCollection<IDecoration> Models
        {
            get => this.models;

            private set
            {
                this.models = value.ToList();
            }
        }

        public void Add(IDecoration model)
        {
            this.models.Add(model);
        }

        public IDecoration FindByType(string type)
        {
            IDecoration decoration = models.FirstOrDefault(x => x.GetType().Name == type);

            return decoration;
        }

        public bool Remove(IDecoration model)
        {
            return this.models.Remove(model);
        }
    }
}
