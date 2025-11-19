
using NumberWordAnalyzer.Queries;

namespace NumberWordAnalyzer.Handlers
{
	public interface INumberWordQueryHandler
	{
		Task<Dictionary<string, int>> GetResults(NumberWordInputQuery query, CancellationToken cancellationToken = default);
	}
}
