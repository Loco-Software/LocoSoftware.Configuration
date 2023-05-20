﻿using System.Diagnostics.CodeAnalysis;
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
public class TestBuilderExtension
{

    /// <summary>
    /// Tests the AddObject Extension Method with Auto Map Enabled
    /// </summary>
    [Test]
    public void TestAddObjectAutoMap()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        TestStruct testObject = new TestStruct();
        testObject.TestProperty = "Hello there,";
        testObject.TestProperty2 = "General Kenobi";
        
        builder.AddObject<TestStruct>(testObject);
        
        IConfigurationRoot configuration = builder.Build();

        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct:TestProperty"));
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct:TestProperty2"));
        
        Assert.That(configuration.GetValue<String>("AttributeTestStruct:TestProperty"), Is.EqualTo("Hello there,"));
        Assert.That(configuration.GetValue<String>("AttributeTestStruct:TestProperty2"), Is.EqualTo("General Kenobi"));
    }
    
    /// <summary>
    /// Tests the AddObject Extension Method with Auto Map Disabled
    /// </summary>
    [Test]
    public void TestAddObjectManualMap()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        TestStruct testObject = new TestStruct();
        testObject.TestProperty = "Hello there,";
        testObject.TestProperty2 = "General Kenobi";

        builder.AddObject<TestStruct>(testObject);

        IConfigurationRoot configuration = builder.Build();
        
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct:TestProperty"));
        Assert.NotNull(configuration.GetValue<String>("AttributeTestStruct:TestProperty2"));
        
        Assert.That(configuration.GetValue<String>("AttributeTestStruct:TestProperty"), Is.EqualTo("Hello there,"));
        Assert.That(configuration.GetValue<String>("AttributeTestStruct:TestProperty2"), Is.EqualTo("General Kenobi"));
    }
    
}