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
        String objectNamespace = Helpers.GetNamespace<T>();
        Boolean autoMap = false; // Helpers.GetAutoMap<T>();

        IEnumerable<PropertyInfo> objectProperties = typeof(T).GetProperties();
        List<MappedPropertyInfo> mappedProperties = new List<MappedPropertyInfo>();

        if (autoMap)
        {
            foreach (PropertyInfo property in objectProperties)
            {
                String propertyName = $"{objectNamespace}:{Helpers.GetObjectName<T>(property.Name, true)}";
                String propertyValue = Helpers.GetPropertyValue(property.Name, obj, true);
                mappedProperties.Add(new MappedPropertyInfo(propertyName, typeof(object), propertyValue));
            }
        }
        else
        {
            foreach (PropertyInfo property in objectProperties)
            {
                if (Helpers.HasAttribute<T, ConfigurationValueAttribute>(property.Name) && !Helpers.HasAttribute<T, IgnoreInConfigurationAttribute>(property.Name))
                {
                    String propertyName = $"{objectNamespace}:{Helpers.GetObjectName<T>(property.Name)}";
                    Type propertyType = Helpers.GetObjectType<T>(property.Name);
                    String propertyValue = Helpers.GetPropertyValue(property.Name, obj);
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
