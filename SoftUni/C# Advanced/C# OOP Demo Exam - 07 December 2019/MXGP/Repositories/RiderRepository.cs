using MXGP.Models.Riders.Contracts;
using MXGP.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Repositories
{
    public class RiderRepository : Repository<IRider>, IRepository<IRider>
    {
        public RiderRepository()
            :base()
        {
        }

        public override IRider GetByName(string name)
        {
            IRider targetRider = (IRider)models.Find(x => x.Name == name);
            return targetRider;
        }
    }
}
