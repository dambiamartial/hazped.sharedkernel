namespace hazped.sharedkernel.Services;

/// <summary>
/// Interface to perform JWT actions
/// </summary>
public interface IJWTService
{
    /// <summary>
    /// Get the user Id
    /// </summary>
    /// <returns>Guid</returns>
    public Guid GetUserId();

    /// <summary>
    /// Get the username
    /// </summary>
    /// <returns>String</returns>
    public string GetUserName();

    /// <summary>
    /// Get the user claim
    /// </summary>
    /// <param name="claim">Claim type name</param>
    /// <returns>String</returns>
    public string GetUserClaim(string claim);
}
