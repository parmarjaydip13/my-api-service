using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Presentation.Extensions
{

    public static class ResultExtensions
    {
        public static IResult ToProblemResult(this Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }

            var problemDetails = new ProblemDetails
            {
                Type = "",
                Status = GetStatusCode(result.Error.ErrorType),
                Title = GetTitle(result.Error.ErrorType),
                Instance = "",
                Detail = result.Error.Description
            };

            return Results.Problem(problemDetails);
        }

        private static int GetStatusCode(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError // Fallback for unknown error types
            };

        private static string GetTitle(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => "Bad Request",
                ErrorType.NotFound => "Not Found",
                ErrorType.Conflict => "Conflict",
                _ => "Unexpected Error" // Fallback for unknown error types
            };
    }
}
