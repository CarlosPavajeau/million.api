using Mapster;
using Million.Categories.Domain;

namespace Million.Categories.Application.FindLowest;

public class FindLowestCategoryQueryHandler : IRequestHandler<FindLowestCategoryQuery, CategoryResponse>
{
    private readonly DbContext _context;

    public FindLowestCategoryQueryHandler(DbContext context)
    {
        _context = context;
    }

    public async Task<CategoryResponse> Handle(FindLowestCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _context.Set<Category>()
            .OrderBy(x => x.Difficulty)
            .Take(1)
            .Select(x => x.Adapt<CategoryResponse>())
            .FirstAsync(cancellationToken);
    }
}
