using SharedKernel;

namespace TodoApplication.API.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToProblemDetails(this Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }

            //return Results.Problem(
            //    statusCode: ,
            //    title: result.Error.ErrorType.ToString(),
                
            //    );
        }
    }
}
