using System.Diagnostics.CodeAnalysis;
using LocoSoftware.Configuration.Attributes;
using LocoSoftware.Configuration.Attributes.Exceptions;

namespace LocoSoftware.Configuration.Testing;

[ExcludeFromCodeCoverage]
[ConfigurationNamespace("AttributeTestStruct")]
file struct TestStruct
{
        
    [ConfigurationValue("TestProperty", typeof(String))]
    public String TestProperty { get; set; }
    
    public String ShouldFailProperty { get; set;  }
        
}

file struct ShouldFailStruct
{
}

[TestFixture]
public class Tests
{
    [Test]
    public void TestHasAttribute()
    {
        Boolean hasAttribute = Helpers.HasAttribute<TestStruct, ConfigurationNamespaceAttribute>();
        Assert.True(hasAttribute);
    }

    [Test]
    public void TestHasAttributeOnProperty()
    {
        Assert.Throws<PropertyNotFoundException>(() =>
            Helpers.HasAttribute<TestStruct, ConfigurationValueAttribute>("NonExistingProperty"));
        Boolean hasAttribute = Helpers.HasAttribute<TestStruct, ConfigurationValueAttribute>("TestProperty");
        Assert.True(hasAttribute);
    }

    [Test]
    public void TestGetNamespaceAttribute()
    {
        Assert.Throws<AttributeNotFoundException>(() => Helpers.GetNamespace<ShouldFailStruct>());
        
        String namespaceName = Helpers.GetNamespace<TestStruct>();
        Assert.That(namespaceName, Is.EqualTo("AttributeTestStruct"));
    }

    [Test]
    public void TestGetObjectName()
    {
        Assert.Throws<AttributeNotFoundException>(() => Helpers.GetObjectName<TestStruct>("ShouldFailProperty"));
        Assert.Throws<PropertyNotFoundException>(() => Helpers.GetObjectName<ShouldFailStruct>("NonExistingProperty"));
        
        String objectName = Helpers.GetObjectName<TestStruct>("TestProperty");
        Assert.That(objectName, Is.EqualTo("TestProperty"));
    }

    [Test]
    public void TestGetObjectType()
    {       
        Assert.Throws<AttributeNotFoundException>(() => Helpers.GetObjectType<TestStruct>("ShouldFailProperty"));
        Assert.Throws<PropertyNotFoundException>(() => Helpers.GetObjectType<ShouldFailStruct>("NonExistingProperty"));

        Type objectType = Helpers.GetObjectType<TestStruct>("TestProperty");
        Assert.That(objectType, Is.EqualTo(typeof(String)));
    }

    [Test]
    public void TestGetPropertyValue()
    {
        TestStruct testStructInstance = new TestStruct();
        testStructInstance.TestProperty = "Hello, World!";
        Assert.Throws<PropertyNotFoundException>(() => Helpers.GetPropertyValue<TestStruct>("NonExistingProperty", testStructInstance));
        // Assert.Throws<MemberAccessException>(() => Helpers.GetPropertyValue<TestStruct>("ShouldFailProperty", testStructInstance));
        Assert.Throws<AttributeNotFoundException>(() => Helpers.GetPropertyValue<TestStruct>("ShouldFailProperty", testStructInstance));

        Object value = Helpers.GetPropertyValue<TestStruct>("TestProperty", testStructInstance);
        String valueAsString = (String)value;
        
        Assert.That(valueAsString, Is.EqualTo("Hello, World!"));

    }
}