using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleASPDotNetCore._MCommands;
using SampleASPDotNetCore._MNotifications;
using SampleASPDotNetCore._MQueries;
using SampleASPDotNetCore.Data;

namespace SampleASPDotNetCore.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class MProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ISender _sender;
        private readonly IPublisher _publisher;
        public MProductController(ISender sender, IPublisher publisher)
        {
            _sender = sender;
            _publisher = publisher;
        }
        [HttpGet]
        public async Task<IActionResult> GetMProducts()
        {
            var products = await _sender.Send(new GetMProductQuery());
            return Ok(products);
        }
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetMProductByID(int ID)
        {
            var products = await _sender.Send(new GetMProductbyIDQuery(ID));
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> AddMProduct([FromBody] MProduct product)
        {
             await _sender.Send(new AddMProductCommand(product));
            await _publisher.Publish(new MProductAddedNotification(product));
            return StatusCode(201);
        }
    }
}
