namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using _02.LegionSystem.Interfaces;
    using Wintellect.PowerCollections;

    public class Legion : IArmy
    {
        private OrderedSet<IEnemy> _enemeies;

        public Legion()
        {
            this._enemeies = new OrderedSet<IEnemy>();
        }
        public int Size => this._enemeies.Count;

        public bool Contains(IEnemy enemy)
        {
            return this._enemeies.Contains(enemy);
        }

        public void Create(IEnemy enemy)
        {
            this._enemeies.Add(enemy);
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            // n*log(n) might have issues
            for (int i = 0; i < this.Size; i++)
            {
                var current = this._enemeies[i];

                if (current.AttackSpeed == speed)
                {
                    return current;
                }
            }

            return null;
        }

        public List<IEnemy> GetFaster(int speed)
        {
            return this._enemeies.FindAll(e => e.AttackSpeed > speed).ToList();
        }

        public IEnemy GetFastest()
        {
            this.EnsureNotEmpty();

            return this._enemeies.GetFirst();
        }

        public IEnemy[] GetOrderedByHealth()
        {
            OrderedBag<IEnemy> enemiesByHealth = new OrderedBag<IEnemy>(this._enemeies, CompareByHealth);

            return enemiesByHealth.ToArray();
        }

        public List<IEnemy> GetSlower(int speed)
        {
            return this._enemeies.FindAll(e => e.AttackSpeed < speed).ToList();
        }

        public IEnemy GetSlowest()
        {
            this.EnsureNotEmpty();

            return this._enemeies.GetLast();
        }

        public void ShootFastest()
        {
            this.EnsureNotEmpty();

            this._enemeies.RemoveFirst();
        }

        public void ShootSlowest()
        {
            this.EnsureNotEmpty();

            this._enemeies.RemoveLast();
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
        }

        private int CompareByHealth(IEnemy enemy1, IEnemy enemy2)
        {
            return enemy2.Health - enemy1.Health;
        }
    }
}
