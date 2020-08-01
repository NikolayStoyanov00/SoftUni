using MXGP.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Repositories
{
    public abstract class Repository<T> : IRepository<T>
    {
        protected List<T> models;

        public Repository()
        {
            models = new List<T>();
        }
        public void Add(T model)
        {
            models.Add(model);
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return models;
        }

        public abstract T GetByName(string name);

        public bool Remove(T model)
        {
            return models.Remove(model);
        }
    }
}
