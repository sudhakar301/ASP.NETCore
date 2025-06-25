using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleASPDotNetCore._MCommands;
using SampleASPDotNetCore._MNotifications;
using SampleASPDotNetCore.Data;
using SampleASPDotNetCore.DToS;

namespace SampleASPDotNetCore.Controllers.V1
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request,IValidator<CreateOrderRequest> validator)
        {
            var validationResults = validator.Validate(request);
            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToDictionary());
            }
            return Ok("Order created successfully");
        }
    }
}
