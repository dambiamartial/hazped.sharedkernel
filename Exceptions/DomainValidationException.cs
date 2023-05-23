namespace hazped.sharedkernel.Exceptions;

/// <summary>
/// Domain's events validation
/// </summary>
public class DomainValidationException : Exception
{
    internal DomainValidationException(string message) : base(message)
    {
        ValidationErrors = new List<string>();
    }
    public List<string> ValidationErrors { get; }
}