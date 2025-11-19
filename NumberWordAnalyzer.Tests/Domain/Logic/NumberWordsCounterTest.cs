using NumberWordAnalyzer.Domain.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace NumberWordAnalyzer.Tests.Domain.Logic
{
	public class NumberWordsCounterTest
	{
		private readonly NumberWordsCounter _processor = new();

		[Fact]
		public async Task ReturnsCorrectCountsForMultipleWords()
		{
			var input = "onetwothreefourfive";
			var result = await _processor.NumberWordProcesser(input);

			Assert.Equal(1, result["one"]);
			Assert.Equal(1, result["two"]);
			Assert.Equal(1, result["three"]);
			Assert.Equal(1, result["four"]);
			Assert.Equal(1, result["five"]);

			// All other counts should be zero
			foreach (var word in result.Keys)
			{
				if (word is not ("one" or "two" or "three" or "four" or "five"))
				{
					Assert.Equal(0, result[word]);
				}
			}
		}

	}
}
