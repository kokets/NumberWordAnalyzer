using FluentValidation;
using NumberWordAnalyzer.Domain.Logic;
using NumberWordAnalyzer.Handlers;
using NumberWordAnalyzer.Validators;

namespace NumberWordAnalyzer.Application
{
	public static class DepencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<NumberWordsCounter>();
			services.AddScoped<INumberWordQueryHandler, NumberWordQueryHandler>();
			services.AddValidatorsFromAssemblyContaining<NumberWordInputQueryValidator>();
			return services;
		}
	}
}
