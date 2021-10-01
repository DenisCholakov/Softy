using System;
using System.Collections.Generic;
using System.Text;

namespace Raiding.Models
{
    class Warrior : BaseHero
    {
        private const int POWER = 100;

        public Warrior(string name) : base(name)
        {
            this.Power = POWER;
        }

        public override string CastAbility() => $"{this.GetType().Name} - {this.Name} hit for {this.Power} damage";
    }
}
