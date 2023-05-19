# LocoSoftware.Configuration

A Simple Library that allows the injection of an ValueType- Object into a ConfigurationBuilder.

Usage:
```csharp

using LocoSoftware.Configuration;
using Microsoft.Extensions.Configuration;

namespace Application;

public static class Program
{

    // [ConfigurationNamespace(...)]  Declares, that this Struct is a Configuration Namespace
    // This does not have to be a Top-level Namespace. It can also be a nested one like Configuration:Module1
    [ConfigurationNamespace("Configuration")] // 
    struct ConfigurationStruct
    {
        // [ConfigurationValue(...)] Declares, that this Property should be mapped in the Configuration
        // First Parameter is the Name, second the Data Type. The last one is currently unused but will be
        // required in a planned Feature.
        [ConfigurationValue("ApplicationName", typeof(String))
        public String ApplicationName { get; set; }
        
        // Properties with the [IgnoreInConfiguration] Attribute will not be mapped and ignored. 
        [IgnoreInConfiguration]
        public String Unrelevant { get; set; }
    
    }

    public static void Main(){
        IConfigurationBuilder builder = new ConfigurationBuilder();
        
        ConfigurationStruct data = new ConfigurationStruct();
        data.ApplicationName = "Sample Application to Demonstrate LocoSoftware.Configuration";
        
        // To add an Object, Call AddObject<TypeOfObject>(InstanceOfTheObject)
        builder.AddObject<ConfigurationStruct>(data);
        
        // The resulting IConfigurationRoot would look like this:
        // Configuration:ApplicationName = "Sample Application to Demonstrate LocoSoftware.Configuration"
        
        IConfigurationRoot config = builder.Build();
        String appName = config.GetValue<String>("Configuration:ApplicationName");
        
        Debug.Assert(appName == "Sample Application to Demonstrate LocoSoftware.Configuration");
        
        // If a nested Namespace would be set e. g.  [ConfigurationNamespace("Configuration:Core")]
        // it would look like that:
        // Configuration:Core:ApplicationName = "Sample Application to Demonstrate LocoSoftware.Configuration"
        
        // All Data Types will be Converted to a String. 
        // This limitation will be removed in a future Release
    }

} 


```