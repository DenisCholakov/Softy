namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {

        private List<IEntity> _entities;

        public Loader()
        {
            this._entities = new List<IEntity>();
        }
        
        public int EntitiesCount => this._entities.Count;

        public void Add(IEntity entity)
        {
            this._entities.Add(entity);
        }

        public void Clear()
        {
            this._entities = new List<IEntity>();
        }

        public bool Contains(IEntity entity)
        {
            return this._entities.IndexOf(entity) != -1;
        }

        public IEntity Extract(int id)
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                var entity = this._entities[i];

                if (entity.Id == id)
                {
                    this._entities.Remove(entity);
                    return entity;
                }
            }

            return null;
        }

        public IEntity Find(IEntity entity)
        {
            int entityIndex = this._entities.IndexOf(entity);

            if (this.EntitiesCount == 0 || entityIndex == -1)
            {
                return null;
            }

            return this._entities[entityIndex];
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this._entities);
        }

        public void RemoveSold()
        {
            this._entities.RemoveAll(e => e.Status == BaseEntityStatus.Sold);
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            int index = this._entities.IndexOf(oldEntity);
            this.ValidateIndex(index);

            this._entities[index] = newEntity;
        }

        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            List<IEntity> result = new List<IEntity>();

            for (int i = 0; i < this.EntitiesCount; i++)
            {
                var current = this._entities[i];

                if (current.Status >= lowerBound && current.Status <= upperBound)
                {
                    result.Add(current);
                }
            }

            return result;
        }

        public void Swap(IEntity first, IEntity second)
        {
            int firstIndex = this._entities.IndexOf(first);
            this.ValidateIndex(firstIndex);
            int secondIndex = this._entities.IndexOf(second);
            this.ValidateIndex(secondIndex);

            var temp = this._entities[firstIndex];
            this._entities[firstIndex] = this._entities[secondIndex];
            this._entities[secondIndex] = temp;
        }

        public IEntity[] ToArray()
        {
            return this._entities.ToArray();
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                var current = this._entities[i];

                if (current.Status == oldStatus)
                {
                    current.Status = newStatus;
                }
            }
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            return this._entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ValidateIndex(int index)
        {
            if (index == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }
        }
    }
}
