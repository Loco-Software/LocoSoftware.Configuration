using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LocoSoftware.Configuration.Attributes;
using Microsoft.Extensions.Configuration;

namespace LocoSoftware.Configuration.Extension
{
    
/// <summary>
/// Contains the Extension Methods to <see cref="IConfigurationBuilder"/>
/// </summary>
public static partial class ConfigurationBuilderExtensions
{
    /// <summary>
    /// Adds a Single Object of Type T
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="obj"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IConfigurationBuilder AddObject<T>(this IConfigurationBuilder builder, T obj)
    {
        // Get Namespace of Object
        ConfigurationNamespaceData namespaceData  = Helpers.GetConfigurationNamespaceData<T>();

        IEnumerable<PropertyInfo> objectProperties = typeof(T).GetProperties();
        List<MappedPropertyInfo> mappedProperties = new List<MappedPropertyInfo>();

        if (namespaceData.AutoMap)
        {
            foreach (PropertyInfo property in objectProperties)
            {
                ConfigurationValueData valueData =
                    Helpers.GetConfigurationValueData(property.Name, obj, namespaceData.AutoMap);
                String propertyName = $"{namespaceData.ObjectNamespace}:{valueData.ObjectName}";
                String propertyValue = valueData.ObjectValue;
                mappedProperties.Add(new MappedPropertyInfo(propertyName, typeof(object), propertyValue));
            }
        }
        else
        {
            foreach (PropertyInfo property in objectProperties)
            {
                if (Helpers.HasAttribute<T, ConfigurationValueAttribute>(property.Name) && !Helpers.HasAttribute<T, IgnoreInConfigurationAttribute>(property.Name))
                {
                    ConfigurationValueData valueData =
                        Helpers.GetConfigurationValueData(property.Name, obj, namespaceData.AutoMap);
                    String propertyName = $"{namespaceData.ObjectNamespace}:{valueData.ObjectName}";
                    Type propertyType = valueData.ObjectType;
                    String propertyValue = valueData.ObjectValue;
                    mappedProperties.Add(new MappedPropertyInfo(propertyName, propertyType, propertyValue));
                }
            }
        }

        IEnumerable<KeyValuePair<String, String>> mappedPairs =
            mappedProperties.Select(e => new KeyValuePair<string, string>(e.Name, e.Value));

        builder.AddInMemoryCollection(mappedPairs);
        
        return builder;
    }
}
}
