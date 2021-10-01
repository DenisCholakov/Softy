using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PokemonDontGo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> pokemons = Console.ReadLine().Split().Select(int.Parse).ToList();

            int sum = 0;
            while (pokemons.Count != 0)
            {
                int index = int.Parse(Console.ReadLine());

                if (index < 0)
                {
                    index = 0;
                    sum += pokemons[index];
                    ChangePokemons(pokemons, index);
                    pokemons.Insert(index, pokemons[pokemons.Count - 1]);
                    pokemons.RemoveAt(index + 1);
                    continue;
                }
                else if (index >= pokemons.Count)
                {
                    index = pokemons.Count - 1;
                    sum += pokemons[index];
                    ChangePokemons(pokemons, index);
                    pokemons.Add(pokemons[0]);
                    pokemons.RemoveAt(index);
                    continue;
                }

                sum += pokemons[index];
                ChangePokemons(pokemons, index);
                pokemons.RemoveAt(index);
            }

            Console.WriteLine(sum);
        }

        private static void ChangePokemons(List<int> pokemons, int index)
        {
            int pokemon = pokemons[index];

            for (int i = 0; i < pokemons.Count; i++)
            {
                if (pokemons[i] <= pokemon)
                {
                    pokemons[i] += pokemon;
                }
                else
                {
                    pokemons[i] -= pokemon;
                }
            }
        }
    }
}
