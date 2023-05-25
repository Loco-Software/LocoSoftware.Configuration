using System;

namespace LocoSoftware.Configuration.Attributes.Exceptions
{
    /// <summary>
    /// Exception thrown when a specified Property does not have the required Attribute
    /// </summary>
    public class AttributeNotFoundException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public AttributeNotFoundException(string message) : base(message)
        {
        }
    }
}

