using Million.Api.Test.Shared;
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
        var categories = JsonConvert.DeserializeObject<List<Category>>(responseString);

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
        categories = JsonConvert.DeserializeObject<List<Category>>(responseString);

        // Assert
        categories.Should().HaveCount(1);
    }
}
