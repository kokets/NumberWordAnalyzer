namespace NumberWordAnalyzer.Queries
{
	public class NumberWordInputQuery
	{
		/// <summary>
		/// The text input to be analyzed
		/// </summary>
		public string InputString { get; }

		public NumberWordInputQuery(string inputString)
		{
			InputString = inputString;
		}
	}
}
