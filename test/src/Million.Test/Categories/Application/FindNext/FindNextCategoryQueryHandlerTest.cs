using Microsoft.EntityFrameworkCore;
using Million.Categories.Application.FindNext;
using Million.Categories.Domain;
using Million.Shared.Infrastructure.Persistence;
using Million.Test.Categories.Domain;

namespace Million.Test.Categories.Application.FindNext;

public class FindNextCategoryQueryHandlerTest
{
    private readonly FindNextCategoryQueryHandler _handler;
    private readonly MillionDbContext _context;

    public FindNextCategoryQueryHandlerTest()
    {
        var dbContextOptions = new DbContextOptionsBuilder<MillionDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new MillionDbContext(dbContextOptions);

        _handler = new FindNextCategoryQueryHandler(_context);
    }
    
    [Fact]
    public async Task ShouldReturnNextCategory()
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
        
        var currentDifficulty = 0;
        
        // Act
        var result = await _handler.Handle(new FindNextCategoryQuery(currentDifficulty), CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        
        // Act
        currentDifficulty = result.Difficulty;
        result = await _handler.Handle(new FindNextCategoryQuery(currentDifficulty), CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Difficulty.Should().BeGreaterThan(currentDifficulty);
    }
}
