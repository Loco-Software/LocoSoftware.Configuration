using System.Reflection;

namespace LocoSoftware.Configuration.Attributes;

/// <summary>
///    Marks Property as a Configuration Value <para />
///    Takes the Path as Optional Argument
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ConfigurationValueAttribute : Attribute
{
    /// <summary>
    /// The Name of the Object <para />
    /// Pulled from nameOf(X) if not specified
    /// </summary>
    public String ObjectName { get; private set; }
    
    /// <summary>
    /// The Type of the Object <para />
    /// "Object" is used if non specified
    /// </summary>
    public Type ObjectType { get; private set;  }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="objectName"></param>
    /// <param name="objectType"></param>
    public ConfigurationValueAttribute(String objectName, Type objectType)
    {
        ObjectName = objectName;
        ObjectType = objectType;
    }
    
}