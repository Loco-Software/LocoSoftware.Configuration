using System;
using System.Diagnostics;
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
    /// Tests the AutoMap Integration of AddObject with existing Data
    /// </summary>
    [Test]
    public void TestAutoMapIntegrationWithData()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        AutoMapStruct testObject = new AutoMapStruct();
        testObject.TestProperty = "Hello there,";
        testObject.TestProperty2 = "General Kenobi";
        
        builder.AddObject<AutoMapStruct>(testObject);
        builder.AddXmlFile("./_TestAssets/ConfigurationSample.xml");
        
        IConfigurationRoot configuration = builder.Build();
        
        Assert.NotNull(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty"));
        Assert.NotNull(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty2"));
        Assert.NotNull(configuration.GetValue<String>("ExampleConfiguration:Value1"));
        Assert.NotNull(configuration.GetValue<String>("ExampleConfiguration:Value2"));
        
        Assert.That(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty"), Is.EqualTo("Hello there,"));
        Assert.That(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty2"), Is.EqualTo("General Kenobi"));
        Assert.That(configuration.GetValue<String>("ExampleConfiguration:Value1"), Is.EqualTo("Value2A"));
        Assert.That(configuration.GetValue<String>("ExampleConfiguration:Value2"), Is.EqualTo("Value1A"));
    }
    
    /// <summary>
    /// Tests the ManualMap Integration of AddObject with existing Data
    /// </summary>
    [Test]
    public void TestManualMapIntegrationWithData()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        ManualMapStruct testObject = new ManualMapStruct();
        testObject.TestProperty = "Hello there,";
        
        builder.AddObject<ManualMapStruct>(testObject);
        builder.AddXmlFile("./_TestAssets/ConfigurationSample.xml");
        
        IConfigurationRoot configuration = builder.Build();
        
        Assert.NotNull(configuration.GetValue<String>("ManualMapStructNamespace:TestProperty"));
        Assert.NotNull(configuration.GetValue<String>("ExampleConfiguration:Value1"));
        Assert.NotNull(configuration.GetValue<String>("ExampleConfiguration:Value2"));
        
        Assert.That(configuration.GetValue<String>("ManualMapStructNamespace:TestProperty"), Is.EqualTo("Hello there,"));
        Assert.That(configuration.GetValue<String>("ExampleConfiguration:Value1"), Is.EqualTo("Value2A"));
        Assert.That(configuration.GetValue<String>("ExampleConfiguration:Value2"), Is.EqualTo("Value1A"));
    }

    /// <summary>
    /// Tests the Integration of AddObject with existing Data being overwritten<para />
    /// Initial Data by a Configuration File being overwritten
    /// </summary>
    [Test]
    public void TestAutoMapIntegrationWithOverrideLast()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        AutoMapStruct testObject = new AutoMapStruct();
        testObject.TestProperty = "Hello there,";
        testObject.TestProperty2 = "General Kenobi";
        
        builder.AddXmlFile("./_TestAssets/AutoMapStruct.xml");
        builder.AddObject<AutoMapStruct>(testObject);
        
        IConfigurationRoot configuration = builder.Build();
        
        Assert.NotNull(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty"));
        Assert.NotNull(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty2"));
        
        Assert.That(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty"), Is.EqualTo("Hello there,"));
        Assert.That(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty2"), Is.EqualTo("General Kenobi"));
    }
    
    /// <summary>
    /// Tests the Integration of AddObject with existing Data being overwritten<para />
    /// Initial Data by a Configuration File being overwritten
    /// </summary>
    [Test]
    public void TestManualMapIntegrationWithOverrideLast()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        ManualMapStruct testObject = new ManualMapStruct();
        testObject.TestProperty = "Hello there,";
        
        builder.AddXmlFile("./_TestAssets/ManualMapStruct.xml");
        builder.AddObject<ManualMapStruct>(testObject);
        
        IConfigurationRoot configuration = builder.Build();
        
        Assert.NotNull(configuration.GetValue<String>("ManualMapStructNamespace:TestProperty"));
        
        Assert.That(configuration.GetValue<String>("ManualMapStructNamespace:TestProperty"), Is.EqualTo("Hello there,"));
    }
    
    /// <summary>
    /// Tests the Integration of AddObject with existing Data being overwritten<para />
    /// Initial Data by a AddObject being overwritten
    /// </summary>
    [Test]
    // [Ignore("Broken. Need to investigate Loading Hierarchy in ConfigurationBuilder")]
    public void TestAutoMapIntegrationWithOverrideFirst()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        AutoMapStruct testObject = new AutoMapStruct();
        testObject.TestProperty = "Hello there,";
        testObject.TestProperty2 = "General Kenobi";
        
        builder.AddObject<AutoMapStruct>(testObject);
        builder.AddXmlFile("./_TestAssets/AutoMapStruct.xml");
        
        IConfigurationRoot configuration = builder.Build();
        
        Assert.NotNull(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty"));
        Assert.NotNull(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty2"));
        
        Assert.That(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty"), Is.EqualTo("TestPropertyValue"));
        Assert.That(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty2"), Is.EqualTo("TestProperty2Value"));
    }
    
    /// <summary>
    /// Tests the Integration of AddObject with existing Data being overwritten<para />
    /// Initial Data by a AddObject being overwritten
    /// </summary>
    [Test]
    // [Ignore("Broken. Need to investigate Loading Hierarchy in ConfigurationBuilder")]
    public void TestManualMapIntegrationWithOverrideFirst()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        ManualMapStruct testObject = new ManualMapStruct();
        testObject.TestProperty = "Hello there,";
        
        builder.AddObject<ManualMapStruct>(testObject);
        builder.AddXmlFile("./_TestAssets/ManualMapStruct.xml");
        
        IConfigurationRoot configuration = builder.Build();
        
        Assert.NotNull(configuration.GetValue<String>("ManualMapStructNamespace:TestProperty"));
        
        Assert.That(configuration.GetValue<String>("ManualMapStructNamespace:TestProperty"), Is.EqualTo("TestPropertyValue"));
    }
}
}
