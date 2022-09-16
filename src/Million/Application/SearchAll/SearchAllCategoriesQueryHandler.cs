using MediatR;
using Million.Domain;

namespace Million.Application.SearchAll;

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
            .Select(x => new CategoryResponse(x.Name, x.Difficulty))
            .ToListAsync(cancellationToken);
    }
}
