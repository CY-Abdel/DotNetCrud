using System.ComponentModel.DataAnnotations;

namespace TicketsCRUD.Core.DTO
{
    public class CreateTicketDto
    {
        public DateTime Time { get; set; }
        public string PassengerName { get; set; }
        public long PassengerSSN { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int price { get; set; }
    }
}
