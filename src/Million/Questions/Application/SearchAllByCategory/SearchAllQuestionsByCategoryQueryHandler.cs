using Mapster;
using Million.Questions.Domain;

namespace Million.Questions.Application.SearchAllByCategory;

public sealed class
    SearchAllQuestionsByCategoryQueryHandler : IRequestHandler<SearchAllQuestionsByCategoryQuery,
        IEnumerable<QuestionResponse>>
{
    private const int MaxQuestionsPerCategory = 5;

    private readonly DbContext _context;

    public SearchAllQuestionsByCategoryQueryHandler(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuestionResponse>> Handle(SearchAllQuestionsByCategoryQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Set<Question>()
            .Include(x => x.Answers)
            .Where(x => x.CategoryId == request.CategoryId)
            .OrderBy(r => Guid.NewGuid())
            .Take(MaxQuestionsPerCategory)
            .Select(x => x.Adapt<QuestionResponse>())
            .ToListAsync(cancellationToken);
    }
}
