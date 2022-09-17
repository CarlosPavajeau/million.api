using Microsoft.EntityFrameworkCore;
using Million.Questions.Application.SearchAllByCategory;
using Million.Shared.Infrastructure.Persistence;
using Million.Test.Questions.Domain;

namespace Million.Test.Questions.Application.SearchAllByCategory;

public class SearchAllQuestionsByCategoryQueryHandlerTest
{
    private readonly SearchAllQuestionsByCategoryQueryHandler _handler;
    private readonly MillionDbContext _context;

    public SearchAllQuestionsByCategoryQueryHandlerTest()
    {
        var dbContextOptions = new DbContextOptionsBuilder<MillionDbContext>()
            .UseInMemoryDatabase("test")
            .Options;
        _context = new MillionDbContext(dbContextOptions);

        _handler = new SearchAllQuestionsByCategoryQueryHandler(_context);
    }

    [Fact]
    public async Task ShouldReturnAllQuestionsByCategory()
    {
        // Arrange
        var question = QuestionMother.Random(1);
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        // Act
        var query = new SearchAllQuestionsByCategoryQuery(1);
        var result = await _handler.Handle(query, CancellationToken.None);
        result = result.ToList();

        // Assert
        result.Should().NotBeNull()
            .And.HaveCount(1);
        result.First().Answers.Should().HaveCount(4);

        // Arrange
        _context.Questions.Add(QuestionMother.Random(2));
        _context.Questions.Add(QuestionMother.Random(2));
        await _context.SaveChangesAsync();

        // Act
        result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull()
            .And.HaveCount(1);

        // Act
        query = new SearchAllQuestionsByCategoryQuery(2);
        result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull()
            .And.HaveCount(2);
    }
}
