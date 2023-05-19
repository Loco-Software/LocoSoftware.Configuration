namespace LocoSoftware.Configuration.Attributes.Exceptions;

/// <summary>
/// Exception thrown when a required Property could not be found.
/// </summary>
public class PropertyNotFoundException : Exception
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    public PropertyNotFoundException(string message) : base(message) {}
}