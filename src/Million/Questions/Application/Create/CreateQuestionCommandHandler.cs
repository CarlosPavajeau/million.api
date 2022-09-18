using Mapster;
using Million.Questions.Domain;

namespace Million.Questions.Application.Create;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, int>
{
    private readonly DbContext _context;

    public CreateQuestionCommandHandler(DbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        if (request.Answers.Count > 4)
        {
            throw new InvalidOperationException("A question can have more than 4 answers");
        }

        var question = request.Adapt<Question>();
        foreach (var answer in question.Answers)
        {
            answer.Question = question;
        }

        await _context.AddAsync(question, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return question.Id;
    }
}
