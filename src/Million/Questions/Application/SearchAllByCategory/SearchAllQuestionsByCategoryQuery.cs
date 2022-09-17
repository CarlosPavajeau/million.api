namespace Million.Questions.Application.SearchAllByCategory;

public sealed record SearchAllQuestionsByCategoryQuery(int CategoryId) : IRequest<IEnumerable<QuestionResponse>>;
