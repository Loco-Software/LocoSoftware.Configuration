using System.Diagnostics.CodeAnalysis;

namespace LocoSoftware.Configuration;

/// <summary>
/// Holds Informations on mapped Properties
/// </summary>
[ExcludeFromCodeCoverage]
public struct MappedPropertyInfo
{
    /// <summary>
    /// Property Name
    /// </summary>
    public String Name { get; }
    
    /// <summary>
    /// Property Type <para />
    /// Not used yet. Included for future Features
    /// </summary>
    public Type Type { get; }
    
    /// <summary>
    /// Property Value as String
    /// </summary>
    public String Value { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public MappedPropertyInfo(String name, Type type, String value)
    {
        Name = name;
        Type = type;
        Value = value;
    }
}