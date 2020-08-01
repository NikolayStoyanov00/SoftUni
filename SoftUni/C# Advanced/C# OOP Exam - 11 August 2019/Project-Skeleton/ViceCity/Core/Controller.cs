using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViceCity.Core.Contracts;
using ViceCity.Models.Guns;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Models.Neighbourhoods;
using ViceCity.Models.Players;
using ViceCity.Repositories;

namespace ViceCity.Core
{
    public class Controller : IController
    {
        public Controller()
        {
            civilPlayers = new List<CivilPlayer>();
            gunRepository = new GunRepository();
            mainPlayer = new MainPlayer();
            gangNeighbourhood = new GangNeighbourhood();
        }

        private List<CivilPlayer> civilPlayers;
        private GunRepository gunRepository;
        private MainPlayer mainPlayer;
        private GangNeighbourhood gangNeighbourhood;
        public string AddGun(string type, string name)
        {
            IGun gun = null;

            if (type is "Pistol")
            {
                gun = new Pistol(name);
            }
            else if (type is "Rifle")
            {
                gun = new Rifle(name);
            }
            else
            {
                return "Invalid gun type!";
            }

            gunRepository.Add(gun);
            return $"Successfully added {gun.Name} of type: {type}";
        }

        public string AddGunToPlayer(string name)
        {
            if (gunRepository.Models.Count == 0)
            {
                return "There are no guns in the queue!";
            }

            IGun gun = gunRepository.Models.FirstOrDefault(x => x.CanFire == true);
            if (name == "Vercetti")
            {
                mainPlayer.GunRepository.Add(gun);
                gunRepository.Remove(gun);
                return $"Successfully added {gun.Name} to the Main Player: Tommy Vercetti";
            }

            if (civilPlayers.Find(x => x.Name == name) == null)
            {
                return "Civil player with that name doesn't exists!";
            }

            CivilPlayer civilPlayer = civilPlayers.Find(x => x.Name == name);
            civilPlayer.GunRepository.Add(gun);
            gunRepository.Remove(gun);
            return $"Successfully added {gun.Name} to the Civil Player: {civilPlayer.Name}";
        }

        public string AddPlayer(string name)
        {
            CivilPlayer civilPlayer = new CivilPlayer(name);

            civilPlayers.Add(civilPlayer);
            return $"Successfully added civil player: {civilPlayer.Name}!";
        }

        public string Fight()
        {
            gangNeighbourhood.Action(mainPlayer, civilPlayers.ToArray());

            bool everyoneIsOkay = true;

            foreach (var civilPlayer in civilPlayers)
            {
                if (civilPlayer.LifePoints < 50)
                {
                    everyoneIsOkay = false;
                }
            }

            if (mainPlayer.LifePoints == 100 && everyoneIsOkay == true)
            {
                return "Everything is okay!";
            }

            else
            {
                int countOfDeadPeople = 0;
                int aliveCivilPlayers = civilPlayers.Where(x => x.IsAlive == true).Count();

                foreach (var civilPlayer in civilPlayers)
                {
                    if (civilPlayer.IsAlive == false)
                    {
                        countOfDeadPeople++;
                    }
                }

                return $"A fight happened:" +
                    $"{Environment.NewLine}Tommy live points: {mainPlayer.LifePoints}!" +
                    $"{Environment.NewLine}Tommy has killed: {countOfDeadPeople} players!" +
                    $"{Environment.NewLine}Left Civil Players: {aliveCivilPlayers}!";
            }
        }
    }
}
