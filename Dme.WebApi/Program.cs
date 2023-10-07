using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dme.Interaction.RepositoryRequests;
using Dme.Interaction.RepositoryRequests.Models;
using Dme.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
	throw new Exception("Default connection string is expected");

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
	.ConfigureContainer<ContainerBuilder>(containerBuilder =>
	{
		containerBuilder.RegisterModule(new WebApiModule(connectionString));
	});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapGet("/users/{id:int}", (int id, 
		[FromServices] IGetUserRequest request, 
		[FromServices] Serilog.ILogger logger) =>
	{
		if (id < 0)
			return Results.BadRequest($"{nameof(id)} must be positive");

		try
		{
			var user = request.GetUser(id);
			return user == null
				? Results.NotFound()
				: Results.Ok(new UserRecord(user.Id, user.Name.First, user.Name.Last, user.BirthDate, user.Picture.Large));
		}
		catch (Exception ex)
		{
			logger.Error(ex, "Error occured when processing get user {id} request", id);
			return Results.StatusCode(500);
		}
	})
	.WithOpenApi();

app.MapGet("/users",
		(int? page, int? pageSize, OrderBy? orderBy, Sort? sort, 
			[FromServices] IGetUsersRequest request,
			[FromServices] Serilog.ILogger logger) =>
		{
			if (page == 0)
				return Results.BadRequest($"{nameof(page)} must be positive or absent");
			if (pageSize == 0)
				return Results.BadRequest($"{nameof(pageSize)} must be positive or absent");
			if (new[] {page, pageSize}.Count(b => b > 0) == 1)
				return Results.BadRequest($"{nameof(page)} and {nameof(pageSize)} must be both positive or absent");

			try
			{
				var users = request.GetUsers(new QueryOptions(page, pageSize, sort switch { Sort.Ascending => true, Sort.Descending => false, _ => null }),
					orderBy == OrderBy.Name, orderBy == OrderBy.BirthDate);
				if (!users.Any())
					return Results.NotFound();

				return Results.Ok(users.Select(user => new UserRecord(user.Id, user.Name.First, user.Name.Last, user.BirthDate, user.Picture.Large)));
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Error occured when processing get users request " +
				                 "with query parameters: {parameters}",
					new {page, pageSize, orderBy, sort});
				return Results.StatusCode(500);
			}
		})
	.WithOpenApi();

app.Run();

internal record UserRecord(int Id, string FirstName, string LastName, DateTimeOffset BirthDate, string PhotoUrl)
{
	public string FullName => string.Join(" ", LastName, FirstName);
}

internal enum OrderBy
{
	BirthDate,
	Name
}

internal enum Sort
{
	Ascending,
	Descending
}