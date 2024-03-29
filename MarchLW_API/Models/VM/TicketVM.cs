namespace MarchLW_API.Models.VM
{
    public class TicketVM
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public decimal TotalPrice { get; set; }
        public List<int> RideIds { get; set; }
    }
}