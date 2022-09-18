using System.Net;
using Million.Api.Test.Shared;
using Million.Categories.Application;
using Million.Categories.Domain;
using Million.Test.Categories.Domain;
using Newtonsoft.Json;

namespace Million.Api.Test.Controllers;

public class CategoriesControllerTest : ApplicationContextTestCase
{
    public CategoriesControllerTest(ApplicationTestCase applicationTestCase) : base(applicationTestCase)
    {
    }

    [Fact]
    public async Task GetCategories()
    {
        // Act
        var response = await Client.GetAsync("/api/categories");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var categories = JsonConvert.DeserializeObject<List<CategoryResponse>>(responseString);

        // Assert
        categories.Should().BeEmpty();

        // Arrange
        var category = CategoryMother.Random();
        var dbContext = GetDbContext();
        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();

        // Act
        response = await Client.GetAsync("/api/categories");
        response.EnsureSuccessStatusCode();

        responseString = await response.Content.ReadAsStringAsync();
        categories = JsonConvert.DeserializeObject<List<CategoryResponse>>(responseString);

        // Assert
        categories.Should().HaveCount(1);
        
        // Cleanup
        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task GetFirst()
    {
        // Arrange
        var categories = new List<Category>
        {
            CategoryMother.Random(),
            CategoryMother.Random(),
            CategoryMother.Random()
        };
        var dbContext = GetDbContext();
        await dbContext.Categories.AddRangeAsync(categories);
        await dbContext.SaveChangesAsync();
        
        var lowestDifficult = categories.Min(c => c.Difficulty);
        
        // Act
        var response = await Client.GetAsync("/api/categories/first");
        response.EnsureSuccessStatusCode();
        
        // Assert
        var responseString = await response.Content.ReadAsStringAsync();
        var category = JsonConvert.DeserializeObject<CategoryResponse>(responseString);
        
        category.Should().NotBeNull();
        category.Difficulty.Should().Be(lowestDifficult);
        
        // Cleanup
        dbContext.Categories.RemoveRange(categories);
        await dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task GetNext()
    {
        // Arrange
        var categories = new List<Category>
        {
            CategoryMother.Random(),
            CategoryMother.Random(),
            CategoryMother.Random()
        };
        var dbContext = GetDbContext();
        await dbContext.Categories.AddRangeAsync(categories);
        await dbContext.SaveChangesAsync();
        
        var lowestDifficult = categories.Min(c => c.Difficulty);
        
        // Act
        var response = await Client.GetAsync($"/api/categories/next/{lowestDifficult}");
        response.EnsureSuccessStatusCode();
        
        // Assert
        var responseString = await response.Content.ReadAsStringAsync();
        var category = JsonConvert.DeserializeObject<CategoryResponse>(responseString);
        
        category.Should().NotBeNull();
        category.Difficulty.Should().BeGreaterThan(lowestDifficult);
        
        // Act
        response = await Client.GetAsync($"/api/categories/next/{int.MaxValue}");

        // Assert - 404
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        
        // Cleanup
        dbContext.Categories.RemoveRange(categories);
        await dbContext.SaveChangesAsync();
    }
}
