namespace LocoSoftware.Configuration.Attributes;

/// <summary>
///    Marks a Struct or Class as a Configuration Namespace <para />
///    Takes the Path as Optional Argument
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class ConfigurationNamespaceAttribute : Attribute
{
    /// <summary>
    /// Namespace of the Object
    /// </summary>
    public String ObjectNamespace { get; private set; }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="objectNamespace"></param>
    public ConfigurationNamespaceAttribute(String objectNamespace)
    {
        this.ObjectNamespace = objectNamespace;
    }
}