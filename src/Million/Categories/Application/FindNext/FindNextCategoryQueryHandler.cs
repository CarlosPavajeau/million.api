using Mapster;
using Million.Categories.Domain;

namespace Million.Categories.Application.FindNext;

public class FindNextCategoryQueryHandler : IRequestHandler<FindNextCategoryQuery, CategoryResponse?>
{
    private readonly DbContext _context;

    public FindNextCategoryQueryHandler(DbContext context)
    {
        _context = context;
    }

    public async Task<CategoryResponse?> Handle(FindNextCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _context.Set<Category>()
            .Where(x => x.Difficulty > request.Difficulty)
            .OrderBy(x => x.Difficulty)
            .Take(1)
            .Select(x => x.Adapt<CategoryResponse>())
            .FirstOrDefaultAsync(cancellationToken);
    }
}
