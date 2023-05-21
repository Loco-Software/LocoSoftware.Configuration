using System;
using System.Reflection;
using LocoSoftware.Configuration.Attributes.Exceptions;

namespace LocoSoftware.Configuration.Attributes
{
    /// <summary>
    /// Holds Data from a ConfigurationValue-Attribute
    /// </summary>
    public struct ConfigurationValueData
    {
        /// <summary>
        /// The Name of the Object <para />
        /// Pulled from nameOf(X) if not specified
        /// </summary>
        public String ObjectName { get;  set; }
        
        /// <summary>
        /// Value
        /// </summary>
        public String ObjectValue { get; set;  }
        
        /// <summary>
        /// The Type of the Object <para />
        /// "Object" is used if non specified
        /// </summary>
        public Type ObjectType { get;  set;  }
    }
    
    public static partial class Helpers
    {
        
        /// <summary>
        /// Reads ConfigurationValueData from an Object Property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="instance"></param>
        /// <param name="autoMap"></param>
        /// <typeparam name="TObjectType"></typeparam>
        /// <returns></returns>
        /// <exception cref="PropertyNotFoundException"></exception>
        /// <exception cref="AttributeNotFoundException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public static ConfigurationValueData GetConfigurationValueData<TObjectType>(String propertyName, TObjectType instance, Boolean autoMap = false)
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
                return new ConfigurationValueData
                {
                    ObjectName = property.Name, 
                    ObjectType = property.PropertyType,
                    ObjectValue = property.GetValue(instance, null).ToString() ?? ""
                };
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

            return new ConfigurationValueData
            {
                ObjectName = attr.ObjectName,
                ObjectType = attr.ObjectType,
                ObjectValue = property.GetValue(instance, null).ToString()
            };
        }
    }
}

