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
    public void TestHasAttributeOnProperty()
    {
        Assert.Throws<PropertyNotFoundException>(() =>
            Helpers.HasAttribute<ManualMapStruct, ConfigurationValueAttribute>("NonExistingProperty"));
        Boolean hasAttribute = Helpers.HasAttribute<ManualMapStruct, ConfigurationValueAttribute>("TestProperty");
        Assert.True(hasAttribute);
    }

    [Test]
    public void TestGetNamespaceAttribute()
    {
        Assert.Throws<AttributeNotFoundException>(() => Helpers.GetNamespace<ShouldFailStruct>());
        
        String namespaceName = Helpers.GetNamespace<ManualMapStruct>();
        Assert.That(namespaceName, Is.EqualTo("AttributeTestStruct"));
    }
    
    [Test]
    public void GetTestAutoMapValue()
    {
        Assert.Throws<AttributeNotFoundException>(() => Helpers.GetNamespace<ShouldFailStruct>());
        
        Boolean namespaceName = Helpers.GetAutoMap<ManualMapStruct>();
        Assert.That(namespaceName, Is.EqualTo(false));
    }

    [Test]
    public void TestGetObjectName()
    {
        Assert.Throws<AttributeNotFoundException>(() => Helpers.GetObjectName<ManualMapStruct>("ShouldFailProperty"));
        Assert.Throws<PropertyNotFoundException>(() => Helpers.GetObjectName<ShouldFailStruct>("NonExistingProperty"));
        
        String objectName = Helpers.GetObjectName<ManualMapStruct>("TestProperty");
        Assert.That(objectName, Is.EqualTo("TestProperty"));
    }

    [Test]
    public void TestGetObjectType()
    {       
        Assert.Throws<AttributeNotFoundException>(() => Helpers.GetObjectType<ManualMapStruct>("ShouldFailProperty"));
        Assert.Throws<PropertyNotFoundException>(() => Helpers.GetObjectType<ShouldFailStruct>("NonExistingProperty"));

        Type objectType = Helpers.GetObjectType<ManualMapStruct>("TestProperty");
        Assert.That(objectType, Is.EqualTo(typeof(String)));
    }

    [Test]
    public void TestGetPropertyValue()
    {
        ManualMapStruct manualMapStructInstance = new ManualMapStruct();
        manualMapStructInstance.TestProperty = "Hello, World!";
        Assert.Throws<PropertyNotFoundException>(() => Helpers.GetPropertyValue<ManualMapStruct>("NonExistingProperty", manualMapStructInstance));
        // Assert.Throws<MemberAccessException>(() => Helpers.GetPropertyValue<TestStruct>("ShouldFailProperty", testStructInstance));
        Assert.Throws<AttributeNotFoundException>(() => Helpers.GetPropertyValue<ManualMapStruct>("ShouldFailProperty", manualMapStructInstance));

        Object value = Helpers.GetPropertyValue<ManualMapStruct>("TestProperty", manualMapStructInstance);
        String valueAsString = (String)value;
        
        Assert.That(valueAsString, Is.EqualTo("Hello, World!"));

    }
}
}
