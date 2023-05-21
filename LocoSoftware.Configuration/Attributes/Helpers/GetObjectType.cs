using System;
using System.Reflection;
using LocoSoftware.Configuration.Attributes.Exceptions;

namespace LocoSoftware.Configuration.Attributes
{
    
    public static partial class Helpers
    {
        /// <summary>
        /// Get the ObjectType Value of the <see cref="ConfigurationValueAttribute"/> of a Type
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="autoMap"></param>
        /// <typeparam name="TObjectType"></typeparam>
        /// <returns></returns>
        /// <exception cref="PropertyNotFoundException"></exception>
        /// <exception cref="AttributeNotFoundException"></exception>
        [Obsolete("Use GetConfigurationValueData() instead")]
        public static Type GetObjectType<TObjectType>(String propertyName, Boolean autoMap = false)
        {
            Type t = typeof(TObjectType);
            PropertyInfo property = t.GetProperty(propertyName);
            if (property == null)
            {
                throw new PropertyNotFoundException(
                    $"Object of Type {nameof(TObjectType)} has no Public Accessible Property {propertyName}");
            }

            if (autoMap)
            {
                return property.PropertyType;
            }
            
            Boolean hasAttribute = HasAttribute<TObjectType, ConfigurationValueAttribute>(propertyName);

            if (!hasAttribute)
            {
                throw new AttributeNotFoundException(
                    $"Property {propertyName} of Object {nameof(TObjectType)} does not have the ConfigurationValueAttribute Attribute!");
            }
        
            ConfigurationValueAttribute attr = property.GetCustomAttribute(typeof(ConfigurationValueAttribute), true) as ConfigurationValueAttribute;

            if (attr == null)
            {
                throw new NullReferenceException("Failed to read Attribute on Object");
            }
        
            return attr.ObjectType;
        }
    }
}
