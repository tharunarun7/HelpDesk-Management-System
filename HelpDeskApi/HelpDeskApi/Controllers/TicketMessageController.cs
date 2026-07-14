using HelpDeskApi.Models;
using HelpDeskApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace HelpDeskApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketMessageController : ControllerBase
    {
        private readonly ITicketMessageService _service;
        private static int hitCount = 0;

        public TicketMessageController(ITicketMessageService service)
        {
            _service = service;
        }

        // Get all messages for a ticket
        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetMessages(int ticketId)
        {
            var messages = await _service.GetMessagesAsync(ticketId);

            return Ok(messages);
        }

        // Send a message
        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageRequest request)
        {
            hitCount++;
            Console.WriteLine($"Controller Hit = {hitCount}");

            // Get username and role from JWT
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            var message = new TicketMessage
            {
                TicketId = request.TicketId,
                Message = request.Message,

                // Admin messages show "Admin"
                // User messages show actual username
                SentBy = role == "Admin" ? "Admin" : username,

                SentAt = DateTime.Now
            };

            await _service.AddMessageAsync(message);

            return Ok(new
            {
                message = "Message Sent Successfully"
            });
        }

    }
}
