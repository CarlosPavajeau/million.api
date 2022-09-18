using MediatR;
using Million.Categories.Application.FindLowest;
using Million.Categories.Application.FindNext;
using Million.Categories.Application.SearchAll;

namespace Million.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await _mediator.Send(new SearchAllCategoriesQuery());

        return Ok(categories);
    }

    [HttpGet("first")]
    public async Task<IActionResult> GetFirst()
    {
        var category = await _mediator.Send(new FindLowestCategoryQuery());

        return Ok(category);
    }

    [HttpGet("next/{difficult:int}")]
    public async Task<IActionResult> GetNext(int difficult)
    {
        var category = await _mediator.Send(new FindNextCategoryQuery(difficult));

        if (category is null)
        {
            return NotFound();
        }

        return Ok(category);
    }
}
