using System;
using System.Diagnostics.CodeAnalysis;
using LocoSoftware.Configuration.Attributes;

namespace LocoSoftware.Configuration.Testing.Mocks
{
    [ExcludeFromCodeCoverage]
    [ConfigurationNamespace("AttributeTestStruct")]
    public struct ManualMapStruct
    {
        
        [ConfigurationValue("TestProperty", typeof(String))]
        public String TestProperty { get; set; }
        
    
        public String ShouldFailProperty { get; set;  }
        
    }
}