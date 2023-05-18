using LocoSoftware.Configuration.Attributes.Exceptions;

namespace LocoSoftware.Configuration.Attributes;

public static partial class Helpers
{
    
    /// <summary>
    /// Get the ObjectNamespace Value of the <see cref="ConfigurationNamespaceAttribute"/> of a Type
    /// </summary>
    /// <typeparam name="TObjectType"></typeparam>
    /// <returns></returns>
    /// <exception cref="AttributeNotFoundException"></exception>
    public static String GetNamespace<TObjectType>()
    {
        ConfigurationNamespaceAttribute? attr = Attribute.GetCustomAttribute(typeof(TObjectType), typeof(ConfigurationNamespaceAttribute)) as ConfigurationNamespaceAttribute;
        if (attr == null)
        {
            throw new AttributeNotFoundException(
                $"Object of Type {nameof(TObjectType)} does not have the {nameof(ConfigurationNamespaceAttribute)}");
        }
        return attr.ObjectNamespace;
    }
}