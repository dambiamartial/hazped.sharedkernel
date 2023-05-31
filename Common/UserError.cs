namespace hazped.sharedkernel.Common;

public record UserError(ErrorCode Code, string Message, string? Details);