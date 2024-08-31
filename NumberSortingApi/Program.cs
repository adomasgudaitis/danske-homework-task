using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NumberSortingApi.Exceptions;
using NumberSortingApi.Handlers;
using NumberSortingApi.Services;
using NumberSortingApi.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISortingService, SortingService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<INumberSortingHandler, NumberSortingHandler>();

// Add exception handlers
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// POST
app.MapPost("/sort", async (INumberSortingHandler handler, HttpContext context) =>
    {
        var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        var isValid = SortingRequestBodyValidator.TryParseAndValidate(requestBody, out var numbers);
        if (!isValid)
        {
            return Results.BadRequest("Invalid request body.");
        }

        await handler.HandleNumberSorting(numbers);

        return Results.NoContent();
    })
    .WithOpenApi(operation =>
    {
        operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["text/plain"] = new()
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "string",
                            Example = new OpenApiString("4 1 2 3 5 9")
                        }
                    }
                }
            };
        return operation;
    })
    .WithName("SortNumbers")
    .Produces(StatusCodes.Status204NoContent)
    .ProducesValidationProblem();

// GET
app.MapGet("/sort", async (IFileService fileService) =>
    {
        var result = await fileService.ReadAsync();
        
        return string.IsNullOrWhiteSpace(result)
            ? Results.NotFound("The sorting result was not found.")
            : Results.Text(result);
    })
    .WithOpenApi()
    .WithName("RetrieveSortedNumbers")
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

app.UseHttpsRedirection();

app.Run();

public partial class Program { }