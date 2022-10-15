using Mapster;

namespace Million.Score.Application.SearchAll;

public class SearchAllScoresQueryHandler : IRequestHandler<SearchAllScoresQuery, IEnumerable<ScoreResponse>>
{
    private const int MaxScores = 10;

    private readonly DbContext _context;

    public SearchAllScoresQueryHandler(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ScoreResponse>> Handle(SearchAllScoresQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Set<Domain.Score>()
            .OrderByDescending(x => x.Value)
            .Take(MaxScores)
            .Select(x => x.Adapt<ScoreResponse>())
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
