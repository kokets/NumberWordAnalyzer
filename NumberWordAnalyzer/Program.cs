using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using NumberWordAnalyzer.Application;
using System.Reflection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddHttpLogging(logging =>
{
	logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseExceptionHandler(errorApp =>
{
	errorApp.Run(async context =>
	{
		context.Response.StatusCode = 500;
		context.Response.ContentType = "application/json";

		var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
		if (errorFeature != null)
		{
			var ex = errorFeature.Error;

			// Log error
			var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
			logger.LogError(ex, "Unhandled exception");

			// Return generic error message
			await context.Response.WriteAsync("{\"message\":\"An unexpected error occurred.\"}");
		}
	});
});

// Enable HTTP request logging (basic metrics)
app.UseHttpLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
