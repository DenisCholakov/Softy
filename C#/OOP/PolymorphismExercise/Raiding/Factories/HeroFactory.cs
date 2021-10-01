using System;
using System.Collections.Generic;
using System.Text;

using Raiding.Models;
using Raiding.Common;

namespace Raiding.Factories
{
    public class HeroFactory
    {
        public BaseHero CreateHero(string name, string type)
        {
            BaseHero hero;

            if (type == "Druid")
            {
                hero = new Druid(name);
            }
            else if (type == "Paladin")
            {
                hero = new Paladin(name);
            }
            else if (type == "Rogue")
            {
                hero = new Rogue(name);
            }
            else if (type == "Warrior")
            {
                hero = new Warrior(name);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidHeroType);
            }

            return hero;
        }
    }
}
