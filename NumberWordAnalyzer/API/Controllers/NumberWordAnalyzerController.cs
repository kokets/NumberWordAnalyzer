using Microsoft.AspNetCore.Mvc;
using NumberWordAnalyzer.Handlers;
using NumberWordAnalyzer.Queries;

namespace NumberWordAnalyzer.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NumberWordAnalyzerController : ControllerBase
	{
		private readonly INumberWordQueryHandler _query;
		public NumberWordAnalyzerController(INumberWordQueryHandler query)
		{
			_query = query;
		}
		/// <summary>
		///  Process an input string and returns the count of each number word found in the text
		/// </summary>
		/// <param name="query">An input containing text</param>
		/// <param name="cancellationToken">A token to cancel operation</param>
		/// <returns> A dictionary where keys are number words and the values are their counts</returns>
		[HttpPost("analyze")]
		public async Task<IActionResult> Analyzer([FromBody] NumberWordInputQuery query, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = await _query.GetResults(query, cancellationToken);

				return Ok(result);
			}
			catch (OperationCanceledException)
			{
				return StatusCode(StatusCodes.Status499ClientClosedRequest);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
			}
		}
	}
}
