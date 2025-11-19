using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
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
		///  This api endpoint procecesses a block of text and return all number words
		/// </summary>
		/// <param name="query"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
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
