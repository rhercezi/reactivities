using Applicatioin.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResault<T>(Resault<T> resault)
        {
            if (resault == null)
                return NotFound();
            if (resault.IsSucces && resault.Value != null)
                return Ok(resault.Value);
            if (resault.IsSucces && resault.Value == null)
                return NotFound();
            return BadRequest(resault.Error);
        }
    }
}