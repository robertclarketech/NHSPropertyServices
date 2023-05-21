using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
	options.AddPolicy("TodoCorsPolicy",
		policyBuilder =>
		{
			policyBuilder
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
		});
});

builder.Services.AddDbContext<TodoContext>((_, dbContextBuilder) =>
	dbContextBuilder.UseInMemoryDatabase("Todo"));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(Program)));
builder.Services.AddScoped<DbInitializer>();
builder.Services.AddTransient<IDateTimeService, DateTimeService>();
builder.Services.AddTransient<ITodoContext, TodoContext>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("TodoCorsPolicy");
app.UseAuthorization();
app.MapControllers();

await using (var scope = app.Services.CreateAsyncScope())
{
	var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
	dbInitializer.Seed(scope.ServiceProvider);
}

app.Use(async (context, next) =>
{
	try
	{
		await next.Invoke();
	}
	catch (ValidationException e)
	{
		context.Response.StatusCode = StatusCodes.Status400BadRequest;
		await context.Response.WriteAsync(JsonSerializer.Serialize(e.Errors));
	}
});

app.Run();