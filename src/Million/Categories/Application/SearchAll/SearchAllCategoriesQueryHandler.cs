using Mapster;
using Million.Categories.Domain;

namespace Million.Categories.Application.SearchAll;

public class SearchAllCategoriesQueryHandler : IRequestHandler<SearchAllCategoriesQuery, IEnumerable<CategoryResponse>>
{
    private readonly DbContext _context;

    public SearchAllCategoriesQueryHandler(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryResponse>> Handle(SearchAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Set<Category>()
            .Select(x => x.Adapt<CategoryResponse>())
            .ToListAsync(cancellationToken);
    }
}
