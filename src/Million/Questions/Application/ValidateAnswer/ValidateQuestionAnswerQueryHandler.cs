using Million.Questions.Domain;

namespace Million.Questions.Application.ValidateAnswer;

public class ValidateQuestionAnswerQueryHandler : IRequestHandler<ValidateQuestionAnswerQuery, bool>
{
    private readonly DbContext _context;

    public ValidateQuestionAnswerQueryHandler(DbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ValidateQuestionAnswerQuery request, CancellationToken cancellationToken)
    {
        return await _context.Set<Answer>()
            .Where(x => x.Id == request.AnswerId)
            .Select(x => x.IsCorrect)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
