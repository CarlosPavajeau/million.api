using Microsoft.EntityFrameworkCore;
using Million.Categories.Application.FindLowest;
using Million.Categories.Domain;
using Million.Shared.Infrastructure.Persistence;
using Million.Test.Categories.Domain;

namespace Million.Test.Categories.Application.FindLowest;

public class FindLowestCategoryQueryHandlerTest
{
    private readonly FindLowestCategoryQueryHandler _handler;
    private readonly MillionDbContext _context;

    public FindLowestCategoryQueryHandlerTest()
    {
        var dbContextOptions = new DbContextOptionsBuilder<MillionDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new MillionDbContext(dbContextOptions);
        
        _handler = new FindLowestCategoryQueryHandler(_context);
    }
    
    [Fact]
    public async Task ShouldReturnLowestCategory()
    {
        // Arrange
        var categories = new List<Category>
        {
            CategoryMother.Random(),
            CategoryMother.Random(),
            CategoryMother.Random()
        };
        await _context.Categories.AddRangeAsync(categories);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _handler.Handle(new FindLowestCategoryQuery(), CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();

        var lowest = categories.OrderBy(x => x.Difficulty).First();
        result.Name.Should().Be(lowest.Name);
    }
}
