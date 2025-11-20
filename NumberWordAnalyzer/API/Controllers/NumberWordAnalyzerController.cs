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
		private readonly ILogger<NumberWordAnalyzerController> _logger;
		public NumberWordAnalyzerController(INumberWordQueryHandler query, ILogger<NumberWordAnalyzerController> logger)
		{
			_query = query;
			_logger = logger;
		}
		/// <summary>
		///  Process an input string and returns the count of each number word found in the text
		/// </summary>
		/// <param name="query">An input containing text</param>
		/// <param name="cancellationToken">A token to cancel operation</param>
		/// <returns> A dictionary where keys are number words and the values are their counts</returns>
		[HttpPost("analyze")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Produces("application/json")]
		public async Task<IActionResult> Analyzer([FromBody] NumberWordInputQuery query, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = await _query.GetResults(query, cancellationToken);
				_logger.LogInformation("Request was successfully completed");
				return Ok(result);
			}
			catch (OperationCanceledException)
			{
				_logger?.LogWarning("Request was cancelled by the client");
				return StatusCode(StatusCodes.Status499ClientClosedRequest);
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex,"Unexpected error while processing the request");
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
			}
		}
	}
}
