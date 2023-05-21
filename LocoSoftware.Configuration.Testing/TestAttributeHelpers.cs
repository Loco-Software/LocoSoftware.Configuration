using System;
using LocoSoftware.Configuration.Testing.Mocks;
using LocoSoftware.Configuration.Attributes;
using LocoSoftware.Configuration.Attributes.Exceptions;
using NUnit.Framework;

namespace LocoSoftware.Configuration.Testing
{
    
[TestFixture]
public class TestAttributeHelpers
{
    [Test]
    public void TestHasAttribute()
    {
        Boolean hasAttribute = Helpers.HasAttribute<ManualMapStruct, ConfigurationNamespaceAttribute>();
        Assert.True(hasAttribute);
    }

    [Test]
    public void TestGetConfigurationNamespaceData()
    {
        // Test on Auto Map Struct
        ConfigurationNamespaceData autoMapNamespaceData = Helpers.GetConfigurationNamespaceData<Mocks.AutoMapStruct>();
        
        // Test on Manual Map Struct
        ConfigurationNamespaceData manualMapNamespaceData =
            Helpers.GetConfigurationNamespaceData<Mocks.ManualMapStruct>();

        // Should fail on Struct without attribute
        Assert.Throws<AttributeNotFoundException>(() => Helpers.GetConfigurationNamespaceData<Mocks.ShouldFailStruct>());
        
        // Null Checks
        Assert.NotNull(autoMapNamespaceData);
        Assert.NotNull(manualMapNamespaceData);
        
        // Check if Values are correct
        Assert.That(autoMapNamespaceData.ObjectNamespace, Is.EqualTo("AutoMapStructNamespace"));
        Assert.That(autoMapNamespaceData.AutoMap, Is.EqualTo(true));
        Assert.That(manualMapNamespaceData.ObjectNamespace, Is.EqualTo("ManualMapStructNamespace"));
        Assert.That(manualMapNamespaceData.AutoMap, Is.EqualTo(false));
    }

    [Test] public void TestGetConfigurationValueDataAutoMap()
    {
        AutoMapStruct autoMapInstance = new AutoMapStruct
        {
            TestProperty = "Hello, World!",
            TestProperty2 = "Hello there, General Kenobi"
        };

        // Test on Auto Map Struct
        ConfigurationNamespaceData autoMapNamespaceData = Helpers.GetConfigurationNamespaceData<Mocks.AutoMapStruct>();
        
        // Test on Manual Map Struct
        ConfigurationNamespaceData manualMapNamespaceData =
            Helpers.GetConfigurationNamespaceData<Mocks.ManualMapStruct>();
        
        // Null Checks
        Assert.NotNull(autoMapNamespaceData);
        Assert.NotNull(manualMapNamespaceData);
        
        // Test on Properties of AutoMapStruct
        ConfigurationValueData autoMapStructProperty1ValueData =
            Helpers.GetConfigurationValueData("TestProperty", autoMapInstance, true);
        ConfigurationValueData autoMapStructProperty2ValueData =
            Helpers.GetConfigurationValueData("TestProperty2", autoMapInstance, true);
        
        // Null Checks
        Assert.NotNull(autoMapStructProperty1ValueData);
        Assert.NotNull(autoMapStructProperty2ValueData);
        
        // Value Checks
        Assert.That(autoMapStructProperty1ValueData.ObjectValue, Is.EqualTo("Hello, World!"));
        Assert.That(autoMapStructProperty2ValueData.ObjectValue, Is.EqualTo("Hello there, General Kenobi"));
    }
    
    
    [Test] public void TestGetConfigurationValueDataManualMap()
    {
        ManualMapStruct manualMapInstance = new ManualMapStruct
        {
            TestProperty = "Hello, World!",
            ShouldFailProperty = "Hello there, General Kenobi"
        };
        
        // Test on Properties of ManualMapStruct
        ConfigurationValueData manualMapStructProperty1ValueData =
            Helpers.GetConfigurationValueData("TestProperty", manualMapInstance);
        
        // Should fail as this property does not have the ConfigurationValue Attribute
        Assert.Throws<AttributeNotFoundException>(() =>
            Helpers.GetConfigurationValueData("ShouldFailProperty", manualMapInstance));
        
        // Null Checks
        Assert.NotNull(manualMapStructProperty1ValueData);
        
        // Value Checks
        Assert.That(manualMapStructProperty1ValueData.ObjectValue, Is.EqualTo("Hello, World!"));
    }
}
}
