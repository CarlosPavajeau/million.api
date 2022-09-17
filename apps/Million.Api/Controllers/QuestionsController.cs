using MediatR;
using Million.Questions.Application.SearchAllByCategory;
using Million.Questions.Application.ValidateAnswer;

namespace Million.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuestionsController(IMediator mediator)
    {
        _mediator = mediator;
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
}
