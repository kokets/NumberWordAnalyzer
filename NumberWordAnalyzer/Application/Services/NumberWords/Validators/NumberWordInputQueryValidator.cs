using FluentValidation;
using NumberWordAnalyzer.Queries;

namespace NumberWordAnalyzer.Validators
{
	public class NumberWordInputQueryValidator : AbstractValidator<NumberWordInputQuery>
	{
		/// <summary>
		/// Validate that the string is not empty it contains letters only
		/// </summary>
		public NumberWordInputQueryValidator()
		{
			RuleFor(x => x.InputString)
				.NotEmpty().WithMessage("Input cannot be empty")
				.Must(str => str.All(char.IsLetter)).WithMessage("Input string must contain letters only");
		}
	}
}
