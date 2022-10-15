using MediatR;
using Million.Score.Application.Create;
using Million.Score.Application.SearchAll;

namespace Million.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScoresController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ScoresController> _logger;

    public ScoresController(IMediator mediator, ILogger<ScoresController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateScoreCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError("Error saving a score: {}", e);
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var scores = await _mediator.Send(new SearchAllScoresQuery());

        return Ok(scores);
    }
}
