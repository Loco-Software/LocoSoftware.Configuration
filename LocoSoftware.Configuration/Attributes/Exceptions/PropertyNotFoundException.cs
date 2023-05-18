namespace LocoSoftware.Configuration.Attributes.Exceptions;

public class PropertyNotFoundException : Exception
{
    public PropertyNotFoundException(string message) : base(message) {}
}