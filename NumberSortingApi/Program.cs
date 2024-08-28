using NumberSortingApi.Exceptions;
using NumberSortingApi.Exceptions.Handlers;
using NumberSortingApi.Handlers;
using NumberSortingApi.Services;
using NumberSortingApi.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISortingService, SortingService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<INumberSortingHandler, NumberSortingHandler>();

// Add exception handlers
builder.Services.AddExceptionHandler<FileProcessingExceptionHandler>();
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

app.MapPost("/sort", async (string body, INumberSortingHandler handler) =>
    {
        var isValid = SortingRequestBodyValidator.TryParseAndValidate(body, out var numbers);
        if (!isValid)
        {
            return Results.BadRequest("Invalid request body.");
        }

        await handler.HandleNumberSortingAsync(numbers);

        return Results.NoContent();
    })
    .WithOpenApi()
    .WithName("SortNumbers")
    .Produces(StatusCodes.Status204NoContent)
    .ProducesValidationProblem();

app.MapGet("/sort", async (IFileService fileService) =>
    {
        var numbers = await fileService.ReadFromFileAsync();
        if (numbers.Count == 0)
        {
            return Results.NotFound("The sorting result was not found.");
        }

        return SortingResponseValidator.IsValid(numbers)
            ? Results.Ok(numbers)
            : Results.Problem("An error occured while processing the request.");
    })
    .WithOpenApi()
    .WithName("RetrieveSortedNumbers")
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

app.UseHttpsRedirection();

app.Run();