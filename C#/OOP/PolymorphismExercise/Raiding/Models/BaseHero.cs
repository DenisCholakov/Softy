using System;
using System.Collections.Generic;
using System.Text;

using Raiding.Models.Interfaces;

namespace Raiding.Models
{
    public abstract class BaseHero : IHero
    {
        protected BaseHero(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
        public virtual int Power { get; protected set; }

        public abstract string CastAbility();
    }
}
