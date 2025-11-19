using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NumberWordAnalyzer.API.Filters
{
	public class ValidationFilter : IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				var firstError = context.ModelState.Values
					.SelectMany(x => x.Errors)
					.Select(x => x.ErrorMessage);
				//.FirstOrDefault() ?? "validation failed";

				//context.Result = new ContentResult
				//{
				//	StatusCode = 400,
				//	Content = firstError,
				//	ContentType = "text/plain"
				//};
				context.Result = new BadRequestObjectResult(firstError);

			}
		}
		public void OnActionExecuted(ActionExecutedContext context) { }
	}
}
