using Mapster;

namespace Million.Score.Application.Create;

public class CreateScoreCommandHandler : IRequestHandler<CreateScoreCommand, int>
{
    private readonly DbContext _context;

    public CreateScoreCommandHandler(DbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateScoreCommand request, CancellationToken cancellationToken)
    {
        var score = request.Adapt<Domain.Score>();

        await _context.Set<Domain.Score>().AddAsync(score, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return score.Id;
    }
}
