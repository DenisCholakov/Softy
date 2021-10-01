using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PokemonTrainer
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Dictionary<string, Trainer> trainers = new Dictionary<string, Trainer>();
            string input = Console.ReadLine();

            while (input != "Tournament")
            {
                string[] catchInfo = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string trainerName = catchInfo[0];
                string pokemonName = catchInfo[1];
                string pokemonElement = catchInfo[2];
                int pokemonHealth = int.Parse(catchInfo[3]);

                if (!trainers.ContainsKey(trainerName))
                {
                    trainers.Add(trainerName, new Trainer(trainerName));
                }

                trainers[trainerName].AddPokemon(new Pokemon(pokemonName, pokemonElement, pokemonHealth));

                input = Console.ReadLine();
            }

            input = Console.ReadLine();

            while (input != "End")
            {
                foreach (var trainer in trainers)
                {
                    trainer.Value.CheckForElement(input);
                }
                input = Console.ReadLine();
            }

            foreach (var pair in trainers.OrderByDescending(x => x.Value.NumberOfBadges))
            {
                Console.WriteLine(pair.Value);
            }
        }
    }
}
