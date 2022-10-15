namespace Million.Score.Application.Create;

public record CreateScoreCommand(int Value, string PlayerName) : IRequest<int>;
