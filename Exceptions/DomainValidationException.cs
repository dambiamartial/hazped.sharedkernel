namespace hazped.sharedkernel.Exceptions;

/// <summary>
/// Domain's events validation
/// </summary>
public partial class DomainValidationException : Exception
{
    public DomainValidationException(string message) : base(message)
    {
        ValidationErrors = new List<string>();
    }
    public List<string> ValidationErrors { get; }
}