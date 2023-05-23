namespace hazped.sharedkernel.Services;

public class JWTService : IJWTService
{
    private readonly IHttpContextAccessor _context;

    public JWTService(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public string GetUserClaim(string claim) => _context.HttpContext.User.Claims.FirstOrDefault(a => a.Type == claim)!.Value;

    public Guid GetUserId() => new(_context.HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)!.Value);

    public string GetUserName() => _context.HttpContext.User.Identity!.Name!;
}