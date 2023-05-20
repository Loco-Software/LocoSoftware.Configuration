using System;

namespace LocoSoftware.Configuration.Attributes
{

    /// <summary>
    ///    Marks a Struct or Class as a Configuration Namespace <para />
    ///    Takes the Path as Optional Argument
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ConfigurationNamespaceAttribute : Attribute
    {
        /// <summary>
        /// Namespace of the Object
        /// </summary>
        public String ObjectNamespace { get; private set; }
    
        /// <summary>
        /// Automatic Mapping of all Properties based on their Name
        /// </summary>
        public Boolean AutoMap { get; private set; }
    
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="objectNamespace">Base Namespace of all nested Objects</param>
        /// <param name="autoMap">Automatic Mapping of all Properties based on their Name</param>
        public ConfigurationNamespaceAttribute(String objectNamespace, Boolean autoMap = false)
        {
            this.ObjectNamespace = objectNamespace;
            this.AutoMap = autoMap;
        }
    }    
}
