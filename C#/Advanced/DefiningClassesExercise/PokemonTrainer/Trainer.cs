using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PokemonTrainer
{
    class Trainer
    {
        public Trainer(string name)
        {
            Name = name;
            NumberOfBadges = 0;
            pokemonsCollection = new List<Pokemon>();
        }

        public string Name { get; set; }
        public int NumberOfBadges { get; set; }
        public List<Pokemon> pokemonsCollection { get; set; }

        public void AddPokemon(Pokemon pokemon)
        {
            pokemonsCollection.Add(pokemon);
        }

        public void CheckForElement(string element)
        {
            if (pokemonsCollection.Any(x => x.Element == element))
            {
                NumberOfBadges++;
            }
            else
            {
                this.DecreesePokemonsHealth();
            }
        }

        private void DecreesePokemonsHealth()
        {
            for (int i = 0; i < pokemonsCollection.Count; i++)
            {
                pokemonsCollection[i].Health -= 10;

                if (pokemonsCollection[i].Health <= 0)
                {
                    pokemonsCollection.RemoveAt(i);
                    i--;
                }
            }
        }

        public override string ToString() => $"{Name} {NumberOfBadges} {pokemonsCollection.Count}";
    }
}
