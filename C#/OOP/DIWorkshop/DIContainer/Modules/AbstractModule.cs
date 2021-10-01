using System;
using System.Collections.Generic;
using System.Text;

namespace DIContainer.Modules
{
    public class AbstractModule : IModule
    {
        private Dictionary<Type, Type> mappings;
        private Dictionary<Type, object> instances;

        protected abstract void Configure();

        public void CreateMapping<TInterface, TImplementation>()
        {
            if (!mappings.ContainsKey(typeof(TInterface)))
            {
                mappings.Add(typeof(TInterface), typeof(TImplementation));
            }
        }

        public TImplementation GetInstance<TImplementation>()
        {
            return (TImplementation)instances[typeof(TImplementation)];
        }

        public void SetInstance<TImplementation>(object instance)
        {
            if (!instances.ContainsKey(typeof(TImplementation)))
            {
                instances.Add(typeof(TImplementation), instance);
            }
        }
    }
}
