using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using LocoSoftware.Configuration.Attributes;
using LocoSoftware.Configuration.Extension;
using Microsoft.Extensions.Configuration;

namespace LocoSoftware.Configuration.Testing;

[ExcludeFromCodeCoverage]
[ConfigurationNamespace("AttributeTestStruct")]
file struct TestStruct
{
        
    [ConfigurationValue("TestProperty", typeof(String))]
    public String TestProperty { get; set; }
    
    [ConfigurationValue("TestProperty2", typeof(String))]
    public String TestProperty2 { get; set;  }
}

[ExcludeFromCodeCoverage]
[ConfigurationNamespace("AttributeTestStruct2", true)]
file struct TestStruct2
{
    public String TestProperty { get; set; }
    public String TestProperty2 { get; set; }
}

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
        TestStruct testObject = new TestStruct();
        testObject.TestProperty = "Hello there,";
        testObject.TestProperty2 = "General Kenobi";
        
        builder.AddObject<TestStruct>(testObject);
        builder.AddXmlFile("./_TestAssets/ConfigurationSample.xml");
        
        IConfigurationRoot configuration = builder.Build();
        
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct:TestProperty"));
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct:TestProperty2"));
        Assert.NotNull(configuration.GetValue<String>("ExampleConfiguration:Value1"));
        Assert.NotNull(configuration.GetValue<String>("ExampleConfiguration:Value2"));
        
        Assert.That(configuration.GetValue<String>("AttributeTestStruct:TestProperty"), Is.EqualTo("Hello there,"));
        Assert.That(configuration.GetValue<String>("AttributeTestStruct:TestProperty2"), Is.EqualTo("General Kenobi"));
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
        TestStruct testObject = new TestStruct();
        testObject.TestProperty = "Hello there,";
        testObject.TestProperty2 = "General Kenobi";
        
        builder.AddXmlFile("./_TestAssets/ConfigurationSample2.xml");
        builder.AddObject<TestStruct>(testObject);
        
        IConfigurationRoot configuration = builder.Build();
        
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct:TestProperty"));
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct:TestProperty2"));
        
        Assert.That(configuration.GetValue<String>("AttributeTestStruct:TestProperty"), Is.EqualTo("Hello there,"));
        Assert.That(configuration.GetValue<String>("AttributeTestStruct:TestProperty2"), Is.EqualTo("General Kenobi"));
    }
    
    /// <summary>
    /// Tests the Integration of AddObject with existing Data being overwritten<para />
    /// Initial Data by a AddObject being overwritten
    /// </summary>
    [Test]
    public void TestIntegrationWithOverride2()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        TestStruct testObject = new TestStruct();
        testObject.TestProperty = "Hello there,";
        testObject.TestProperty2 = "General Kenobi";
        
        builder.AddObject<TestStruct>(testObject);
        builder.AddXmlFile("./_TestAssets/ConfigurationSample2.xml");
        
        IConfigurationRoot configuration = builder.Build();
        
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct:TestProperty"));
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct:TestProperty2"));
        
        Assert.That(configuration.GetValue<String>("AttributeTestStruct:TestProperty"), Is.EqualTo("Value2A"));
        Assert.That(configuration.GetValue<String>("AttributeTestStruct:TestProperty2"), Is.EqualTo("Value1A"));
    }
    
}