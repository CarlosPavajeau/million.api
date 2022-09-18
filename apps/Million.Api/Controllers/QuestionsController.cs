using MediatR;
using Million.Questions.Application.Create;
using Million.Questions.Application.SearchAllByCategory;
using Million.Questions.Application.ValidateAnswer;

namespace Million.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<QuestionsController> _logger;

    public QuestionsController(IMediator mediator, ILogger<QuestionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{category:int}")]
    public async Task<IActionResult> GetQuestions(int category)
    {
        var questions = await _mediator.Send(new SearchAllQuestionsByCategoryQuery(category));
        return Ok(questions);
    }

    [HttpGet("validate/{answerId:int}")]
    public async Task<IActionResult> ValidateAnswer(int answerId)
    {
        var isValid = await _mediator.Send(new ValidateQuestionAnswerQuery(answerId));
        return Ok(isValid);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCommand command)
    {
        try
        {
            var questionId = await _mediator.Send(command);
            return Ok(questionId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating question");
            return BadRequest(e.Message);
        }
    }
}
