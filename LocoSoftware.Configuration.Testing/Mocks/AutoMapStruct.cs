using System;
using System.Diagnostics.CodeAnalysis;
using LocoSoftware.Configuration.Attributes;

namespace LocoSoftware.Configuration.Testing.Mocks
{
    [ExcludeFromCodeCoverage]
    [ConfigurationNamespace("AutoMapStructNamespace", true)]
    public struct AutoMapStruct
    {
        public String TestProperty { get; set; }
        public String TestProperty2 { get; set; }
    }
}