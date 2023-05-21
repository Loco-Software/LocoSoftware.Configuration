using System;
using LocoSoftware.Configuration.Testing.Mocks;
using LocoSoftware.Configuration.Extension;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LocoSoftware.Configuration.Testing
{
    
[TestFixture]
public class TestBuilderExtensionIntegration
{

    /// <summary>
    /// Tests the Integration of AddObject with existing Data
    /// </summary>
    [Test]
    public void TestIntegrationWithData()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        AutoMapStruct testObject = new AutoMapStruct();
        testObject.TestProperty = "Hello there,";
        testObject.TestProperty2 = "General Kenobi";
        
        builder.AddObject<AutoMapStruct>(testObject);
        builder.AddXmlFile("./_TestAssets/ConfigurationSample.xml");
        
        IConfigurationRoot configuration = builder.Build();
        
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct2:TestProperty"));
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct2:TestProperty2"));
        Assert.NotNull(configuration.GetValue<String>("ExampleConfiguration:Value1"));
        Assert.NotNull(configuration.GetValue<String>("ExampleConfiguration:Value2"));
        
        Assert.That(configuration.GetValue<String>("AttributeTestStruct2:TestProperty"), Is.EqualTo("Hello there,"));
        Assert.That(configuration.GetValue<String>("AttributeTestStruct2:TestProperty2"), Is.EqualTo("General Kenobi"));
        Assert.That(configuration.GetValue<String>("ExampleConfiguration:Value1"), Is.EqualTo("Value2A"));
        Assert.That(configuration.GetValue<String>("ExampleConfiguration:Value2"), Is.EqualTo("Value1A"));
    }

    /// <summary>
    /// Tests the Integration of AddObject with existing Data being overwritten<para />
    /// Initial Data by a Configuration File being overwritten
    /// </summary>
    [Test]
    public void TestIntegrationWithOverride()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        AutoMapStruct testObject = new AutoMapStruct();
        testObject.TestProperty = "Hello there,";
        testObject.TestProperty2 = "General Kenobi";
        
        builder.AddXmlFile("./_TestAssets/ConfigurationSample2.xml");
        builder.AddObject<AutoMapStruct>(testObject);
        
        IConfigurationRoot configuration = builder.Build();
        
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct2:TestProperty"));
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct2:TestProperty2"));
        
        Assert.That(configuration.GetValue<String>("AttributeTestStruct2:TestProperty"), Is.EqualTo("Hello there,"));
        Assert.That(configuration.GetValue<String>("AttributeTestStruct2:TestProperty2"), Is.EqualTo("General Kenobi"));
    }
    
    /// <summary>
    /// Tests the Integration of AddObject with existing Data being overwritten<para />
    /// Initial Data by a AddObject being overwritten
    /// </summary>
    [Test]
    [Ignore("Broken. Need to investigate Loading Hierarchy in ConfigurationBuilder")]
    public void TestIntegrationWithOverride2()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        AutoMapStruct testObject = new AutoMapStruct();
        testObject.TestProperty = "Hello there,";
        testObject.TestProperty2 = "General Kenobi";
        
        builder.AddObject<AutoMapStruct>(testObject);
        builder.AddXmlFile("./_TestAssets/ConfigurationSample2.xml");
        
        IConfigurationRoot configuration = builder.Build();
        
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct2:TestProperty"));
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct2:TestProperty2"));
        
        Assert.That(configuration.GetValue<String>("AttributeTestStruct2:TestProperty"), Is.EqualTo("Value2A"));
        Assert.That(configuration.GetValue<String>("AttributeTestStruct2:TestProperty2"), Is.EqualTo("Value1A"));
    }
    
}
}
