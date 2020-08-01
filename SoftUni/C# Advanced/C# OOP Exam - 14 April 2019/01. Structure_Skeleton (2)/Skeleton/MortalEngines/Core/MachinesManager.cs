namespace MortalEngines.Core
{
    using Contracts;
    using MortalEngines.Entities;
    using MortalEngines.Entities.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class MachinesManager : IMachinesManager
    {
        private List<IPilot> pilots;
        private List<IMachine> machines;

        public MachinesManager()
        {
            this.pilots = new List<IPilot>();
            this.machines = new List<IMachine>();
        }
        public string HirePilot(string name)
        {
            if (pilots.Any(x => x.Name == name && x.GetType().Name == nameof(Pilot)))
            {
                return $"Pilot {name} is hired already";
            }

            IPilot pilot = new Pilot(name);
            pilots.Add(pilot);
            return $"Pilot {name} hired";
        }

        public string ManufactureTank(string name, double attackPoints, double defensePoints)
        {
            if (machines.Any(m => m.Name == name && m.GetType().Name == nameof(Tank)))
            {
                return $"Machine {name} is manufactured already";
            }

            ITank tank = new Tank(name, attackPoints, defensePoints);
            machines.Add(tank);
            return $"Tank {name} manufactured - attack: {attackPoints:F2}; defense: {defensePoints:F2}";
        }

        public string ManufactureFighter(string name, double attackPoints, double defensePoints)
        {

            if (machines.Any(m => m.Name == name && m.GetType().Name == nameof(Fighter)))
            {
                return $"Machine {name} is manufactured already";
            }

            IFighter fighter = new Fighter(name, attackPoints, defensePoints);
            machines.Add(fighter);
            return $"Fighter {name} manufactured - attack: {fighter.AttackPoints:F2}; defense: {fighter.DefensePoints:F2}; aggressive: ON";
        }

        public string EngageMachine(string selectedPilotName, string selectedMachineName)
        {
            IPilot pilot = pilots.FirstOrDefault(x => x.Name == selectedPilotName);
            IMachine machine = machines.FirstOrDefault(x => x.Name == selectedMachineName);

            if (pilot == null)
            {
                return $"Pilot {selectedPilotName} could not be found";
            }

            if (machine == null)
            {
                return $"Machine {selectedMachineName} could not be found";
            }

            if (machine.Pilot != null)
            {
                return $"Machine {selectedMachineName} is already occupied";
            }

            pilot.AddMachine(machine);
            machine.Pilot = pilot;
            return $"Pilot {pilot.Name} engaged machine {machine.Name}";
        }

        public string AttackMachines(string attackingMachineName, string defendingMachineName)
        {
            IMachine attackingMachine = machines.FirstOrDefault(x => x.Name == attackingMachineName);
            IMachine defendingMachine = machines.FirstOrDefault(x => x.Name == defendingMachineName);

            if (attackingMachine == null)
            {
                return $"Machine {attackingMachineName} could not be found";
            }

            if (defendingMachine == null)
            {
                return $"Machine {defendingMachineName} could not be found";
            }

            if (attackingMachine.HealthPoints <= 0)
            {
                return $"Dead machine {attackingMachineName} cannot attack or be attacked";
            }

            if (defendingMachine.HealthPoints <= 0)
            {
                return $"Dead machine {defendingMachineName} cannot attack or be attacked";
            }

            attackingMachine.Attack(defendingMachine);
            return $"Machine {defendingMachineName} was attacked by machine {attackingMachineName} - current health: {defendingMachine.HealthPoints:F2}";
        }

        public string PilotReport(string pilotReporting)
        {
            IPilot searchedPilot = pilots.FirstOrDefault(x => x.Name == pilotReporting && x.GetType().Name == nameof(Pilot));

            return searchedPilot.Report();
        }

        public string MachineReport(string machineName)
        {
            IMachine machine = machines.FirstOrDefault(x => x.Name == machineName);
            return machine.ToString();
        }

        public string ToggleFighterAggressiveMode(string fighterName)
        {
            if (this.machines.FirstOrDefault(m => m.Name == fighterName && m.GetType().Name == nameof(Fighter)) == null)
            {
                return $"Machine {fighterName} could not be found";
            }

            IFighter fighter = (Fighter)this.machines.FirstOrDefault(m => m.Name == fighterName && m.GetType().Name == nameof(Fighter));

            fighter.ToggleAggressiveMode();
            return $"Fighter {fighterName} toggled aggressive mode";
        }

        public string ToggleTankDefenseMode(string tankName)
        {
            Tank tank = (Tank)machines.FirstOrDefault(x => x.Name == tankName);
               
            if (tank == null)
            {
                return $"Machine {tankName} could not be found";
            }

            tank.ToggleDefenseMode();
            return $"Tank {tank.Name} toggled defense mode";
        }
    }
}