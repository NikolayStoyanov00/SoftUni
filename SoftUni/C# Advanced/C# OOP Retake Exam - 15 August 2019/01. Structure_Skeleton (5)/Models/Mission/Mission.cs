using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Planets;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            while (true)
            {
                IAstronaut astronaut = astronauts.FirstOrDefault(x => x.CanBreath == true);

                if (astronaut == null)
                {
                    break;
                }

                var item = planet.Items.FirstOrDefault();

                if (item == null)
                {
                    break;
                }

                astronaut.Bag.Items.Add(item);
                planet.Items.Remove(item);
                astronaut.Breath();

                if (astronaut.CanBreath == false)
                {
                    astronaut = astronauts.FirstOrDefault(x => x.CanBreath == true);

                    if (astronaut == null)
                    {
                        break;
                    }
                }
            }
            
        }
    }
}
