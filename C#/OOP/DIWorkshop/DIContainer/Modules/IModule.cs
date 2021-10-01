using System;
using System.Collections.Generic;
using System.Text;

namespace DIContainer.Modules
{
    public interface IModule
    {
        public void CreateMapping<TInterface, TImplementation>();

        protected abstract void Configure();

        public void SetInstance<TImplementation>(object instance);

        public TImplementation GetInstance<TImplementation>();
    }
}
