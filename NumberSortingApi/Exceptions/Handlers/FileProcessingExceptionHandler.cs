using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace NumberSortingApi.Exceptions.Handlers;

public class FileProcessingExceptionHandler : IExceptionHandler
{
    private readonly ILogger<FileProcessingExceptionHandler> _logger;

    public FileProcessingExceptionHandler(ILogger<FileProcessingExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not FileProcessingException fileProcessingException)
        {
            return false;
        }

        _logger.Log(LogLevel.Error,
            $"Exception occurred: {fileProcessingException.Message}",
            fileProcessingException.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = "An error occurred while processing your request",
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return true;
    }
}