using System;
using System.Collections.Generic;
using System.Linq;

namespace HW09._Pokemon_Trainer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Trainer> trainers = new List<Trainer>();
            List<Pokemon> pokemons = new List<Pokemon>();
            while (true)
            {
                string command = Console.ReadLine();

                if (command == "Tournament")
                {
                    break;
                }

                string[] tokens = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string trainerName = tokens[0];
                string pokemonName = tokens[1];
                string pokemonElement = tokens[2];
                int pokemonHealth = int.Parse(tokens[3]);

                Pokemon pokemon = new Pokemon(pokemonName, pokemonElement, pokemonHealth);
                pokemons.Add(pokemon);
                bool trainerIsFound = false;
                for (int i = 0; i < trainers.Count; i++)
                {
                    var currentTrainer = trainers[i];

                    if (currentTrainer.Name == trainerName)
                    {
                        trainers[i].Pokemons.Add(pokemon);
                        trainerIsFound = true;
                    }
                }

                if (trainerIsFound == false)
                {
                    Trainer trainer = new Trainer(trainerName, 0, pokemons);
                    trainers.Add(trainer);
                }
                pokemons = new List<Pokemon>();

            }

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "End")
                {
                    break;
                }

                string element = command;

                if (element == "Fire")
                {
                    ChecksForPokemonsWithGivenElement(trainers, element);
                    
                }

                else if (command == "Water")
                {
                    ChecksForPokemonsWithGivenElement(trainers, element);
                }

                else if (command == "Electricity")
                {
                    ChecksForPokemonsWithGivenElement(trainers, element);
                }
            }

            foreach (var trainer in trainers.OrderByDescending(x => x.NumberOfBadges))
            {
                Console.WriteLine($"{trainer.Name} {trainer.NumberOfBadges} {trainer.Pokemons.Count}");
            }
        }

        private static void ChecksForPokemonsWithGivenElement(List<Trainer> trainers, string element)
        {
            bool hasPokemonWithElement = false;
            foreach (var trainer in trainers)
            {
                foreach (var pokemon in trainer.Pokemons)
                {
                    if (pokemon.Element == element)
                    {
                        trainer.NumberOfBadges++;
                        hasPokemonWithElement = true;
                        break;
                    }
                }

                if (hasPokemonWithElement == false)
                {
                    trainer.Pokemons.ForEach(x => x.Health -= 10);
                    RemovesPokemonsWithZeroHP(trainer.Pokemons, trainer);
                }
                hasPokemonWithElement = false;
            }
        }

        private static void RemovesPokemonsWithZeroHP(List<Pokemon> pokemons, Trainer trainer)
        {
            for (int i = 0; i < trainer.Pokemons.Count; i++)
            {
                var pokemon = trainer.Pokemons[i];

                if (pokemon.Health <= 0)
                {
                    trainer.Pokemons.RemoveAt(i);
                }
            }
        }
    }
}
