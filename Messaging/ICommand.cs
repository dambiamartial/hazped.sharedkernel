namespace hazped.sharedkernel.Messaging;

/// <summary>
/// Marker interface to represent a command with a void response
/// </summary>
public interface ICommand : IRequest<Unit> { }

/// <summary>
/// Marker interface to represent a command with a response
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
public interface ICommand<TResponse> : IRequest<TResponse> { }