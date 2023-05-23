namespace hazped.sharedkernel.Behaviours;

public class AppLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly Stopwatch _timer;
    public AppLoggingBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _timer = new Stopwatch();
        _validators = validators;
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        string uniqueId = Guid.NewGuid().ToString();

        //Request
        _logger.LogInformation("Beginning Request {requestName} with Id {uniqueId} on {day} at {time}", requestName, uniqueId, DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now));

        _timer.Start();

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();


        if (failures.Count != 0)
        {
            _logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", requestName, request, failures);
            throw new ValidationException(failures);
        }

        var response = await next();
        _timer.Stop();

        //Response
        _logger.LogInformation("Handled {response}", typeof(TResponse).Name);

        _logger.LogInformation("Ending Request {requestName} with Id {uniqueId} on {day} at {time} with execution time:{timer}ms", requestName, uniqueId, DateOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now), _timer.ElapsedMilliseconds);

        return response;
    }
}