using System.ComponentModel.DataAnnotations.Schema;
namespace HelpDeskApi.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = "Open";

        // NEW
        public string Priority { get; set; } = "Medium";

        public int CreatedBy { get; set; }
        // Navigation property
        public User? CreatedByUser { get; set; }
        // Display username in UI
        [NotMapped]
        public string? CreatedByUsername { get; set; }

        [NotMapped]
        public int AssignedTo { get; set; }

        public string? ScreenshotPath { get; set; }

        [NotMapped]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}