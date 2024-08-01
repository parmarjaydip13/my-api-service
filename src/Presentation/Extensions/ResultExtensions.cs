using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Presentation.Extensions
{

    public static class ResultExtensions
    {
        public static IResult ToProblemResult(this Result result) =>
            result switch
            {
                { IsSuccess: true } => throw new InvalidOperationException(),

                IValidationResult validationResult =>
                  CreateProblemDetails(result.Error, validationResult.Errors),

                _ => CreateProblemDetails(result.Error)
            };

        private static int GetStatusCode(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError // Fallback for unknown error types
            };

        private static string GetType(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1" // Fallback for unknown error types
            };

        private static IResult CreateProblemDetails(Error error, Error[]? errors = null)
        {
            var problemDetails = new ProblemDetails
            {
                Title = error.ErrorType.ToString(),
                Status = GetStatusCode(error.ErrorType),
                Type = GetType(error.ErrorType),
                Detail = error.Description,
            };

            if (errors != null)
            {
                problemDetails.Extensions[nameof(errors)] = errors;
            }
            return Results.Problem(problemDetails);
        }
    }
}
