using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Raiding.Core.Interfaces;
using Raiding.Factories;
using Raiding.Models;

namespace Raiding.Core
{
    class Engine : IEngine
    {
        private readonly HeroFactory _heroFactory;

        public Engine()
        {
            this._heroFactory = new HeroFactory();
        }

        public void Run()
        {
            List<BaseHero> raidGroup = new List<BaseHero>();
            int n = int.Parse(Console.ReadLine());

            while (raidGroup.Count != n)
            {
                try
                {
                    BaseHero hero = this.ProcessHeroInfo();
                    raidGroup.Add(hero);
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
                
            }

            int bossPower = int.Parse(Console.ReadLine());

            foreach (var hero in raidGroup)
            {
                Console.WriteLine(hero.CastAbility());
            }

            int groupPower = raidGroup.Sum(g => g.Power);

            if (groupPower >= bossPower)
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }
        }

        private BaseHero ProcessHeroInfo()
        {
            string name = Console.ReadLine();
            string type = Console.ReadLine();

            return this._heroFactory.CreateHero(name, type);
        }
    }
}
