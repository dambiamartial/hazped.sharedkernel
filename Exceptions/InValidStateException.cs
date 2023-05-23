namespace hazped.sharedkernel.Exceptions;

/// <summary>
/// Aggregates state validation
/// </summary>
public class InValidStateException : Exception
{
    internal InValidStateException(string message) : base(message)
    {
        Errors = new List<string>();
    }
    public List<string> Errors { get; }
}