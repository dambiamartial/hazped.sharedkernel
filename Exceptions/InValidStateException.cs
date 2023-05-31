namespace hazped.sharedkernel.Exceptions;

/// <summary>
/// Aggregates state validation
/// </summary>
public partial class InValidStateException : Exception
{
    public InValidStateException(string message) : base(message)
    {
        Errors = new List<string>();
    }
    public List<string> Errors { get; }
}