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
		public async Task Should_ReturnZeroCounts_WhenInputIsEmpty()
		{
			var result = await _processor.NumberWordProcesser("");

			Assert.All(result, pair => Assert.Equal(0, pair.Value));
		}

		[Fact]
		public async Task Should_ReturnOne_ForSingleNumberWord()
		{
			var result = await _processor.NumberWordProcesser("one");

			Assert.Equal(1, result["one"]);
			Assert.Equal(0, result["two"]);
			Assert.Equal(0, result["three"]);
		}

		[Fact]
		public async Task Should_CountMultipleWordsCorrectly()
		{
			var input = "one two two three";
			var result = await _processor.NumberWordProcesser(input);

			Assert.Equal(1, result["one"]);
			Assert.Equal(2, result["two"]);
			Assert.Equal(1, result["three"]);
		}

		
		[Fact]
		public async Task Should_DetectNumbersFromScrambledLetters()
		{
			var input = "owteno";
			var result = await _processor.NumberWordProcesser(input);

			Assert.Equal(1, result["one"]);
			Assert.Equal(1, result["two"]);
		}

		[Fact]
		public async Task Should_HandleMixedCaseInput()
		{
			var result = await _processor.NumberWordProcesser("OnE TwO ThReE");

			Assert.Equal(1, result["one"]);
			Assert.Equal(1, result["two"]);
			Assert.Equal(1, result["three"]);
		}
	}
}
