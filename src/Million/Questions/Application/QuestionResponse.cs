namespace Million.Questions.Application;

public sealed record AnswerResponse(int Id, string Content);
public sealed record QuestionResponse(string Content, float Reward, ICollection<AnswerResponse> Answers);
