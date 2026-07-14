using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskApi.Models
{
    public class TicketMessage
    {
        [Key]
        public int Id { get; set; }

        public int TicketId { get; set; }

        [ForeignKey("TicketId")]
        public Ticket? Ticket { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;

        [Required]
        public string SentBy { get; set; } = string.Empty;
        // "User" or "Admin"

        public DateTime SentAt { get; set; } = DateTime.Now;
        public string Username { get; set; } = string.Empty;
    }
}
