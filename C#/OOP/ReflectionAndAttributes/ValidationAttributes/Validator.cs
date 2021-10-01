using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ValidationAttributes
{
    public static class Validator
    {
        public static bool IsValid(object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                IEnumerable<MyValidationAttribute> propertyCustomAttributes = property.GetCustomAttributes()
                    .Where(x => x is MyValidationAttribute)
                    .Cast<MyValidationAttribute>();

                foreach (var customAttribute in propertyCustomAttributes)
                {
                    bool result = customAttribute.IsValid(property.GetValue(obj));

                    if (!result)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
