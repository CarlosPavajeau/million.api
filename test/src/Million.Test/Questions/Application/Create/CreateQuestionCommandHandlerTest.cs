using Microsoft.EntityFrameworkCore;
using Million.Questions.Application.Create;
using Million.Shared.Infrastructure.Persistence;
using Million.Test.Questions.Domain;

namespace Million.Test.Questions.Application.Create;

public class CreateQuestionCommandHandlerTest
{
    private readonly CreateQuestionCommandHandler _handler;
    private readonly MillionDbContext _context;

    public CreateQuestionCommandHandlerTest()
    {
        var dbContextOptions = new DbContextOptionsBuilder<MillionDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new MillionDbContext(dbContextOptions);

        _handler = new CreateQuestionCommandHandler(_context);
    }

    [Fact]
    public async Task ShouldCreateQuestion()
    {
        // Arrange
        var question = QuestionMother.Random(1);
        var answers = question.Answers
            .Select(x => new CreateAnswerCommand(x.Content, x.IsCorrect))
            .ToList();
        var command = new CreateQuestionCommand(question.Content, question.Reward, answers);

        // Act
        var questionId = await _handler.Handle(command, CancellationToken.None);

        // Assert
        var found = await _context.Questions.FindAsync(questionId);
        found.Should().NotBeNull();
    }
}
