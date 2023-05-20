using System.Reflection;
using LocoSoftware.Configuration.Attributes.Exceptions;

namespace LocoSoftware.Configuration.Attributes;

public static partial class Helpers
{
    
    /// <summary>
    /// Gets the Value of a Property by Name if the Property has the <see cref="ConfigurationValueAttribute"/>
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="instance"></param>
    /// <param name="checkAttribute"></param>
    /// <typeparam name="TObjectType"></typeparam>
    /// <returns></returns>
    /// <exception cref="PropertyNotFoundException"></exception>
    /// <exception cref="AttributeNotFoundException"></exception>
    public static String GetPropertyValue<TObjectType>(String propertyName, TObjectType instance, Boolean checkAttribute = true)
    {
        Type t = typeof(TObjectType);
        PropertyInfo? property = t.GetProperty(propertyName);
        
        if (property == null)
        {
            throw new PropertyNotFoundException(
                $"Object of Type {nameof(TObjectType)} has no Public Accessible Property {propertyName}");
        }

        if (checkAttribute)
        {
            Boolean hasAttribute = HasAttribute<TObjectType, ConfigurationValueAttribute>(propertyName);
            if (!hasAttribute)
            {
                throw new AttributeNotFoundException(
                    $"Property {propertyName} of Object {nameof(TObjectType)} does not have the ConfigurationValueAttribute Attribute!");
            }
        }
        
        String? propertyValue = property.GetValue(instance, null)?.ToString();
        
        return propertyValue;
    }
    
}