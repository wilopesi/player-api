using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Player.API.Filters
{
	public class ExceptionFilter : IActionFilter
	{

		public ExceptionFilter()
		{

		}

		public void OnActionExecuting(ActionExecutingContext context)
		{

		}

		public void OnActionExecuted(ActionExecutedContext context)
		{

			if (context.Exception != null)
			{
				var exception = context.Exception;
				var exceptionType = exception.GetType();
				var exceptionDetails = exception.ToString();
				HttpStatusCode status = HttpStatusCode.InternalServerError;
				var message = exceptionDetails.ToString();
				if (exceptionType == typeof(UnauthorizedAccessException))
				{
					message = "Access to the web api is not authorized";
					status = HttpStatusCode.Unauthorized;
				}
				else if (exceptionType == typeof(HttpRequestException))
				{
					message = exception.Message + "\n" + exception.StackTrace;
					status = HttpStatusCode.InternalServerError;
				}
				var statusCode = Convert.ToInt32(status);
				var responseData = new
				{
					ErrorCode = statusCode,
					ErrorMsg = message
				};

				context.Result = new ObjectResult(responseData)
				{
					StatusCode = statusCode
				};
				context.ExceptionHandled = true;

			}
		}

	}
}
