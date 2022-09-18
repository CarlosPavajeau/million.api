namespace Million.Questions.Application.Create;

public record CreateAnswerCommand(string Content, bool IsCorrect);

public record CreateQuestionCommand
    (string Content, float Reward, ICollection<CreateAnswerCommand> Answers) : IRequest<int>
{
    public int CategoryId { get; set; }
}
