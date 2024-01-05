using System.ComponentModel.DataAnnotations;

namespace TicketsCRUD.Core.Entities
{
    public class Ticket
    {
        [Key]
        public long Id { get; set; }
        public DateTime Time { get; set; }
        public string PassengerName { get; set; }
        public long PassengerSSN { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int price { get; set; }
        public DateTime CreatedAT { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string ConfidentialComment { get; set; } = "Normal";

    }
}
