using MediatR;
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
}
