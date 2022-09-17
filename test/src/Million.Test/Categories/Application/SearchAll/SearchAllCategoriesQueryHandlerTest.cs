using Microsoft.EntityFrameworkCore;
using Million.Categories.Application.SearchAll;
using Million.Shared.Infrastructure.Persistence;
using Million.Test.Categories.Domain;

namespace Million.Test.Categories.Application.SearchAll;

public class SearchAllCategoriesQueryHandlerTest
{
    private readonly SearchAllCategoriesQueryHandler _handler;
    private readonly MillionDbContext _context;

    public SearchAllCategoriesQueryHandlerTest()
    {
        var dbContextOptions = new DbContextOptionsBuilder<MillionDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new MillionDbContext(dbContextOptions);

        _handler = new SearchAllCategoriesQueryHandler(_context);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfCategories()
    {
        // Arrange with no categories
        var query = new SearchAllCategoriesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
        
        // Arrange with categories
        await _context.Categories.AddAsync(CategoryMother.Random());
        await _context.SaveChangesAsync();
        
        // Act
        result = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeEmpty();
    }
}
