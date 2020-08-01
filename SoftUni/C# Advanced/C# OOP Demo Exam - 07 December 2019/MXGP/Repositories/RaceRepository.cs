using MXGP.Models.Races;
using MXGP.Models.Races.Contracts;
using MXGP.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Repositories
{
    public class RaceRepository : Repository<IRace>, IRepository<IRace>
    {
        public RaceRepository()
            :base()
        {
        }

        public override IRace GetByName(string name)
        {
            var targetRace = (IRace)models.Find(x => x.Name == name);
            return targetRace;
        }
    }
}
