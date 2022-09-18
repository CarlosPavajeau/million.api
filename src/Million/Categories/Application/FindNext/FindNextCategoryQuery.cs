namespace Million.Categories.Application.FindNext;

public record FindNextCategoryQuery(int Difficulty) : IRequest<CategoryResponse?>;
