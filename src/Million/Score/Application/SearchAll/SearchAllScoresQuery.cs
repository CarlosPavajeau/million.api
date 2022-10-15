namespace Million.Score.Application.SearchAll;

public record SearchAllScoresQuery() : IRequest<IEnumerable<ScoreResponse>>;
