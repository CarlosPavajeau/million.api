using MediatR;
using Million.Questions.Application.SearchAllByCategory;

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

    [HttpGet("{category}")]
    public async Task<IActionResult> GetQuestions(int category)
    {
        var questions = await _mediator.Send(new SearchAllQuestionsByCategoryQuery(category));
        return Ok(questions);
    }
}
