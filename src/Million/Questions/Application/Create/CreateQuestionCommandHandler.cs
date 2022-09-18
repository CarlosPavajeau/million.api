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
            throw new InvalidOperationException("A question can't have more than 4 answers");
        }
        
        if (request.Answers.Count(x => x.IsCorrect) > 1)
        {
            throw new InvalidOperationException("A question can't have more than 1 correct answer");
        }

        var question = request.Adapt<Question>();

        await _context.AddAsync(question, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return question.Id;
    }
}
