using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;

namespace NumberSortingApi.Tests;

public class ApiTests
{
    [Fact]
    public async Task PostSortEndpoint_Valid_Input_Returns204NoContent()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();
        const string requestBody = "3 5 1 2 7";

        // Act
        var response =
            await client.PostAsync("/sort", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Theory]
    [InlineData("30 50 1 2 70")]
    [InlineData("-3 -50 0 2 70")]
    [InlineData("11")]
    [InlineData("")]
    [InlineData("a b c d")]
    [InlineData("30 50 1 2 d")]
    public async Task PostSortEndpoint_InvalidInput_Returns400BadRequest(string requestBody)
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();
        
        // Act
        var response =
            await client.PostAsync("/sort", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetSortEndpoint_LoadedValidContent_ReturnsLastLoadedContent()
    {
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();
        const string requestBody = "3 5 1 2 7";
        const string expectedResponseBody = "1 2 3 5 7";

        await client.PostAsync("/sort", new StringContent(requestBody, Encoding.UTF8, "application/json"));    
        
        // Act
        var response = await client.GetAsync("/sort");
        var responseBody = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(expectedResponseBody, responseBody);
    }

    [Fact]
    public async Task GetSortEndpoint_ContentNotLoaded_Returns404NotFound()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();
        
        if (Directory.Exists(Path.Combine(AppContext.BaseDirectory, "Files")))
        {
            Directory.Delete(Path.Combine(AppContext.BaseDirectory, "Files"), true);
        }
        
        // Act
        var response = await client.GetAsync("/sort");
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}