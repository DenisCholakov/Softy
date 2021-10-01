using System;
using System.Collections.Generic;
using System.Text;

namespace DIContainer.Attributes
{
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
    public class Inject : Attribute
    {
    }
}
