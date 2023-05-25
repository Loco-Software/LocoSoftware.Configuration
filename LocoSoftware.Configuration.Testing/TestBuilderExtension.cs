using System;
using LocoSoftware.Configuration.Extension;
using LocoSoftware.Configuration.Testing.Mocks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LocoSoftware.Configuration.Testing
{
    
    [TestFixture]
    public class TestBuilderExtension
    {

        /// <summary>
        /// Tests the AddObject Extension Method with Auto Map Enabled
        /// </summary>
        [Test]
        // [Ignore("Feature Broken At the Moment. AutoMap will be disabled for now")]
        public void TestAddObjectAutoMap()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            AutoMapStruct testObject = new AutoMapStruct();
            testObject.TestProperty = "Hello there,";
            testObject.TestProperty2 = "General Kenobi";
        
            builder.AddObject<AutoMapStruct>(testObject);
        
            IConfigurationRoot configuration = builder.Build();

            Assert.NotNull(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty"));
            Assert.NotNull(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty2"));
        
            Assert.That(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty"), Is.EqualTo("Hello there,"));
            Assert.That(configuration.GetValue<String>("AutoMapStructNamespace:TestProperty2"), Is.EqualTo("General Kenobi"));
        }
    
        /// <summary>
        /// Tests the AddObject Extension Method with Auto Map Disabled
        /// </summary>
        [Test]
        public void TestAddObjectManualMap()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            ManualMapStruct manualMapObject = new ManualMapStruct();
            manualMapObject.TestProperty = "Hello there, General Kenobi";

            builder.AddObject<ManualMapStruct>(manualMapObject);

            IConfigurationRoot configuration = builder.Build();
        
            Assert.NotNull(configuration.GetValue<String>("ManualMapStructNamespace:TestProperty"));
        
            Assert.That(configuration.GetValue<String>("ManualMapStructNamespace:TestProperty"), Is.EqualTo("Hello there, General Kenobi"));
        }
    }
}
