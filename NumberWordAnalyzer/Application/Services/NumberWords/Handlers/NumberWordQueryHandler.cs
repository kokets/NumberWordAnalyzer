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


		public async Task<Dictionary<string, int>> GetResults(NumberWordInputQuery query, CancellationToken cancellationToken = default)
		{
			return await _wordsBL.NumberWordProcesser(query.InputString);
		}
	}
}
