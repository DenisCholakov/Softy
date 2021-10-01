namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    public class Inventory : IHolder
    {
        private List<IWeapon> _weapons;

        public Inventory()
        {
            this._weapons = new List<IWeapon>();
        }
        public int Capacity => this._weapons.Count;

        public void Add(IWeapon weapon)
        {
            this._weapons.Add(weapon);
        }

        public void Clear()
        {
            this._weapons = new List<IWeapon>();
        }

        public bool Contains(IWeapon weapon)
        {
            return this._weapons.Contains(weapon);
        }

        public void EmptyArsenal(Category category)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                var weapon = this._weapons[i];

                if (weapon.Category == category)
                {
                    weapon.Ammunition = 0;
                }

            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            int index = this._weapons.IndexOf(weapon);
            this.ValidateIndex(index);

            var current = this._weapons[index];

            if (current.Ammunition >= ammunition)
            {
                current.Ammunition -= ammunition;
                return true;
            }
            else
            {
                return false;
            }
        }

        public IWeapon GetById(int id)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                var current = this._weapons[i];

                if (current.Id == id)
                {
                    return current;
                }
            }

            return null;
        }

        public IEnumerator GetEnumerator()
        {
            return this._weapons.GetEnumerator();
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            int index = this._weapons.IndexOf(weapon);
            this.ValidateIndex(index);

            var current = this._weapons[index];
            current.Ammunition += ammunition;

            if (current.MaxCapacity < current.Ammunition)
            {
                current.Ammunition = current.MaxCapacity;
            }

            return current.Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            // may have an issue 
            var current = this.GetById(id);

            if (current == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            this._weapons.Remove(current);
            return current;
        }

        public int RemoveHeavy()
        {
            return this._weapons.RemoveAll(w => w.Category == Category.Heavy);
        }

        public List<IWeapon> RetrieveAll()
        {
            return new List<IWeapon>(this._weapons);
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            List<IWeapon> result = new List<IWeapon>();

            for (int i = 0; i < this.Capacity; i++)
            {
                var current = this._weapons[i];

                if (current.Category >= lower && current.Category <= upper)
                {
                    result.Add(current);
                }
            }

            return result;
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            int firstIndex = this._weapons.IndexOf(firstWeapon);
            this.ValidateIndex(firstIndex);
            int secondIndex = this._weapons.IndexOf(secondWeapon);
            this.ValidateIndex(secondIndex);

            if (firstWeapon.Category == secondWeapon.Category)
            {
                var temp = this._weapons[firstIndex];
                this._weapons[firstIndex] = this._weapons[secondIndex];
                this._weapons[secondIndex] = temp;
            }
        }

        private void ValidateIndex(int index)
        {
            if (index == -1)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }
    }
}
