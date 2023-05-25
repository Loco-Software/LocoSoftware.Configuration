using System;
using LocoSoftware.Configuration.Attributes.Exceptions;

namespace LocoSoftware.Configuration.Attributes
{
    /// <summary>
    /// Holds Data from a ConfigurationNamespace-Attribute
    /// </summary>
    public struct ConfigurationNamespaceData
    {
        /// <summary>
        /// Namespace of the Object
        /// </summary>
        public String ObjectNamespace { get; set; }
    
        /// <summary>
        /// Automatic Mapping of all Properties based on their Name
        /// </summary>
        public Boolean AutoMap { get; set; }
    }
    
    public static partial class Helpers
    {
        /// <summary>
        /// Gets the AutoMap Value of the <see cref="ConfigurationNamespaceAttribute"/> of a Type
        /// </summary>
        /// <typeparam name="TObjectType"></typeparam>
        /// <returns></returns>
        /// <exception cref="AttributeNotFoundException"></exception>
        public static ConfigurationNamespaceData GetConfigurationNamespaceData<TObjectType>()
        {
            ConfigurationNamespaceAttribute attr =
                Attribute.GetCustomAttribute(typeof(TObjectType), typeof(ConfigurationNamespaceAttribute)) as
                    ConfigurationNamespaceAttribute;
        
            if (attr == null)
            {
                throw new AttributeNotFoundException(
                    $"Object of Type {nameof(TObjectType)} does not have the {nameof(ConfigurationNamespaceAttribute)}");
            }

            return new ConfigurationNamespaceData { ObjectNamespace = attr.ObjectNamespace, AutoMap = attr.AutoMap };
        }
    }
}

