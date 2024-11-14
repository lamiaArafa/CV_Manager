using ArtStudio.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using ArtStudio.Application.Common.Wrapper;
using System.Net;

public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<CustomExceptionFilterAttribute> _logger;

    public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        if (exception is BadRequestException badRequestException)
        {
           // _logger.LogWarning(badRequestException, $"Bad Request Exception occurred: {badRequestException.Message}");

            var erroResult = Result<string>.Failure(badRequestException.Message);

            context.Result = new ObjectResult(erroResult)
            { StatusCode = (int)HttpStatusCode.BadRequest };
        }
        else
        {
            _logger.LogError(exception, "An unexpected exception occurred");

            var erroResult = Result<string>.Failure("Internal Server Error", exception);

            context.Result = new ObjectResult(erroResult)
            { StatusCode = (int)HttpStatusCode.InternalServerError };
        }

        context.ExceptionHandled = true;
    }
}