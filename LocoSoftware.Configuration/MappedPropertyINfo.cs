using System.Diagnostics.CodeAnalysis;

namespace LocoSoftware.Configuration;

/// <summary>
/// Holds Informations on mapped Properties
/// </summary>
[ExcludeFromCodeCoverage]
public struct MappedPropertyInfo
{
    public String Name { get; }
    public Type Type { get; }
    public String Value { get; }

    public MappedPropertyInfo(String name, Type type, String value)
    {
        Name = name;
        Type = type;
        Value = value;
    }
}