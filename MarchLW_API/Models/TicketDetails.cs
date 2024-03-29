using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarchLW_API.Models
{
    public class TicketDetails
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(TicketID))]
        public int TicketID { get; set; }
        [ForeignKey(nameof(CustomerID))]
        public int CustomerID { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public decimal TotalPrice { get; set; }

        // Navigation properties
        public Tickets Ticket { get; set; }
        public Customer Customer { get; set; }
    }
}
