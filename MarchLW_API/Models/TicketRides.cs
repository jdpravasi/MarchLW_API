using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarchLW_API.Models
{
    public class TicketRides
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(TicketID))]
        public int TicketID { get; set; }
        [ForeignKey(nameof(RideID))]
        public int RideID { get; set; }

        [ValidateNever]
        public Rides ride{ get; set; }
   
        [ValidateNever]
        public Tickets ticket { get; set; }
    }
}
