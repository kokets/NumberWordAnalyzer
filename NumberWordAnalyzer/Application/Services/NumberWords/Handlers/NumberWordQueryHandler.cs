using NumberWordAnalyzer.Domain.Logic;
using NumberWordAnalyzer.Queries;

namespace NumberWordAnalyzer.Handlers
{
	public class NumberWordQueryHandler : INumberWordQueryHandler
	{
		private readonly NumberWordsCounter _wordsBL;
		public NumberWordQueryHandler()
		{
			_wordsBL = new NumberWordsCounter();
		}

		/// <summary>
		/// Processes the input string and return a dictionary containing a count of each number words
		/// </summary>
		/// <param name="query"> An input containing text</param>
		/// <param name="cancellationToken">A token to cancel operations</param>
		/// <returns>A dictionary where keys are number words and the values are their counts</returns>
		public async Task<Dictionary<string, int>> GetResults(NumberWordInputQuery query, CancellationToken cancellationToken = default)
		{
			return await _wordsBL.NumberWordProcesser(query.InputString);
		}
	}
}
