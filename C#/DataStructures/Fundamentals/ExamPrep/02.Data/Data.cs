namespace _02.Data
{
    using _02.Data.Interfaces;
    using _02.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Wintellect.PowerCollections;

    public class Data : IRepository
    {

        public OrderedBag<IEntity> _entities;

        public Data()
        {
            this._entities = new OrderedBag<IEntity>();
        }

        public Data(Data copy)
        {
            this._entities = copy._entities;
        }

        public int Size => this._entities.Count;

        public void Add(IEntity entity)
        {
            this._entities.Add(entity);

            if (entity.ParentId != null)
            {
                var parent = this.GetById((int)entity.ParentId);

                if (parent != null)
                {
                    parent.Children.Add(entity);
                }
            }
        }

        public IRepository Copy()
        {
            Data copy = (Data)this.MemberwiseClone();

            return new Data(copy);
        }

        public IEntity DequeueMostRecent()
        {
            this.EnsureNotEmpty();

            return this._entities.RemoveFirst();
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this._entities);
        }

        public List<IEntity> GetAllByType(string type)
        {
            this.ValidateType(type);

            var result = new List<IEntity>();

            for (int i = 0; i < this.Size; i++)
            {
                var current = this._entities[i];

                if (current.GetType().Name == type)
                {
                    result.Add(current);
                }
            }

            return result;
        }

        public IEntity GetById(int id)
        {

            if (id < 0 || id >= this.Size)
            {
                return null;
            }

            return this._entities[this.Size - 1 - id];
        }

        public List<IEntity> GetByParentId(int parentId)
        {
            if (parentId < 0 || parentId >= this.Size)
            {
                return new List<IEntity>();
            }

            return this.GetById(parentId).Children;
        }

        public IEntity PeekMostRecent()
        {
            this.EnsureNotEmpty();

            return this._entities.GetFirst();
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }
        }

        private void ValidateType(string type)
        {
            if (type != typeof(Invoice).Name && type != typeof(StoreClient).Name && type != typeof(User).Name)
            {
                throw new InvalidOperationException($"Invalid type: {type}");
            }
        }

    }
}
