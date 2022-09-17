namespace Million.Questions.Application.ValidateAnswer;

public record ValidateQuestionAnswerQuery(int AnswerId) : IRequest<bool>;
