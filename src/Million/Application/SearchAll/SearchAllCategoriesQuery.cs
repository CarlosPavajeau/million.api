using MediatR;

namespace Million.Application.SearchAll;

public record SearchAllCategoriesQuery : IRequest<IEnumerable<CategoryResponse>>;
