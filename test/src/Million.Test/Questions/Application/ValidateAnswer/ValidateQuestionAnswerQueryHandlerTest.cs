using Microsoft.EntityFrameworkCore;
using Million.Questions.Application.ValidateAnswer;
using Million.Shared.Infrastructure.Persistence;
using Million.Test.Questions.Domain;

namespace Million.Test.Questions.Application.ValidateAnswer;

public class ValidateQuestionAnswerQueryHandlerTest
{
    private readonly ValidateQuestionAnswerQueryHandler _handler;
    private readonly MillionDbContext _context;

    public ValidateQuestionAnswerQueryHandlerTest()
    {
        var dbContextOptions = new DbContextOptionsBuilder<MillionDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new MillionDbContext(dbContextOptions);

        _handler = new ValidateQuestionAnswerQueryHandler(_context);
    }

    [Fact]
    public async Task ShouldReturnTrueWhenAnswerIsCorrect()
    {
        // Arrange
        var question = QuestionMother.Random(1);
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        // Act
        var query = new ValidateQuestionAnswerQuery(question.Answers.First().Id);
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeFalse();

        // Act
        query = new ValidateQuestionAnswerQuery(question.Answers.First(x => x.IsCorrect).Id);
        result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }
}
