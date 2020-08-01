using MXGP.Models.Motorcycles.Contracts;
using MXGP.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Repositories
{
    public class MotorcycleRepository : Repository<IMotorcycle>, IRepository<IMotorcycle>
    {
        public MotorcycleRepository()
            :base()
        {
        }

        public override IMotorcycle GetByName(string name)
        {
            var targetMotorcycle = (IMotorcycle)this.models.Find(x => x.Model == name);
            return targetMotorcycle;
        }
    }
}
