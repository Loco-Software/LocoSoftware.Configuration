using System.Reflection;
using LocoSoftware.Configuration.Attributes.Exceptions;

namespace LocoSoftware.Configuration.Attributes;

public static partial class Helpers
{

    public static Object GetPropertyValue<TObjectType>(String propertyName, TObjectType instance)
    {
        Type t = typeof(TObjectType);
        PropertyInfo? property = t.GetProperty(propertyName);
        
        if (property == null)
        {
            throw new PropertyNotFoundException(
                $"Object of Type {nameof(TObjectType)} has no Public Accessible Property {propertyName}");
        }
        
        Boolean hasAttribute = HasAttribute<TObjectType, ConfigurationValueAttribute>(propertyName);
        if (!hasAttribute)
        {
            throw new AttributeNotFoundException(
                $"Property {propertyName} of Object {nameof(TObjectType)} does not have the ConfigurationValueAttribute Attribute!");
        }
        
        Object? propertyValue = property.GetValue(instance, null);
        
        return propertyValue;
    }
    
}