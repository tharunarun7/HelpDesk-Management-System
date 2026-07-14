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
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IWebHostEnvironment _environment;

        public TicketController(
            ITicketService ticketService,
            IWebHostEnvironment environment)
        {
            _ticketService = ticketService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role == "Admin")
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                return Ok(tickets);
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var ticketsForUser =
                await _ticketService.GetTicketsByCreatedByAsync(userId);

            return Ok(ticketsForUser);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            await _ticketService.AddTicketAsync(ticket);

            return Ok(new
            {
                message = "Ticket Created Successfully"
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
                return BadRequest();

            await _ticketService.UpdateTicketAsync(ticket);

            return Ok(new
            {
                message = "Ticket Updated Successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            await _ticketService.DeleteTicketAsync(id);

            return Ok(new
            {
                message = "Ticket Deleted Successfully"
            });
        }

        // ==========================
        // Upload Screenshot API
        // ==========================
        [HttpPost("UploadScreenshot")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadScreenshot(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new
                {
                    message = "Please select an image."
                });
            }

            var extension = Path.GetExtension(file.FileName).ToLower();

            var allowedExtensions = new[]
  {
    ".jpg",
    ".jpeg",
    ".png",
    ".pdf",
    ".doc",
    ".docx"
};

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest(new
                {
                    message = "Only JPG, JPEG, PDF, DOC, DOCX and PNG files are allowed."
                });
            }

            var uploadsFolder = Path.Combine(
                _environment.WebRootPath,
                "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + extension;

            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new
            {
                fileName = fileName
            });
        }
    }
}