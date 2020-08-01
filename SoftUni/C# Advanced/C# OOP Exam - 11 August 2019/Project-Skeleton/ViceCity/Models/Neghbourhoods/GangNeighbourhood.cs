using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViceCity.Models.Guns;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Models.Neighbourhoods.Contracts;
using ViceCity.Models.Players;
using ViceCity.Models.Players.Contracts;

namespace ViceCity.Models.Neighbourhoods
{
    public class GangNeighbourhood : INeighbourhood
    {
        public void Action(IPlayer mainPlayer, ICollection<IPlayer> civilPlayers)
        {
            while (true)
            {
                IGun gun = mainPlayer.GunRepository.Models.FirstOrDefault(x => x.CanFire == true);

                if (gun == null)
                {
                    break;
                }

                IPlayer currentCivilPlayer = civilPlayers.FirstOrDefault(x => x.IsAlive == true);

                if (currentCivilPlayer == null)
                {
                    break;
                }

                int damagePoints = gun.Fire();
                currentCivilPlayer.TakeLifePoints(damagePoints);
            }

            while (true)
            {
                IPlayer player = civilPlayers.FirstOrDefault(x => x.IsAlive == true);

                if (player == null)
                {
                    break;
                }

                IGun gun = player.GunRepository.Models.FirstOrDefault(x => x.CanFire == true);

                if (gun == null)
                {
                    break;
                }

                int damagePoints = gun.Fire();
                mainPlayer.TakeLifePoints(damagePoints);

                if (mainPlayer.IsAlive == false)
                {
                    break;
                }
            }
        }
    }
}
