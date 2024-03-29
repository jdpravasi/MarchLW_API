using MarchLW_API.Models;
using MarchLW_API.Models.VM;
using MarchLW_API.Repository;
using MarchLW_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MarchLW_API.Controllers
{
    [Route("api/Tickets")]
    [ApiController]
    public class TicketsController : Controller
    {
        private readonly IRides<Rides> ridesRepo;
        public TicketsController(IRides<Rides> rides)
        {
            ridesRepo = rides;
        }
        [HttpGet("GetAllRides")]
        public ActionResult<IEnumerable<Rides>> GetRides()
        {
            var rides = ridesRepo.GetRides();
            if (rides != null)
            {
                return Ok(rides);
            }
            else
            {
                return NotFound("Cannot Get Rides");
            }

        }
        
        [HttpGet("SearchRides")]
        public ActionResult<IEnumerable<Rides>> SearchRides(string searchQuery)
        {
            var rides = ridesRepo.SearchRides(searchQuery);
            if (rides != null)
            {
                return Ok(rides);
            }
            else
            {
                return NotFound("Cannot Get Rides");
            }

        }

        [HttpPost("CreateTicket")]
        public async Task<IActionResult> CreateTicket(TicketVM model)
        {
            try
            {
                await ridesRepo.CreateTicket(model);
                return Ok(new { message = "Ticket created successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, new { message = "An error occurred while creating the ticket", error = ex.Message });
            }
        }

        [HttpGet("GetAllTicketDetails")]
        public async Task<ActionResult<IEnumerable<TicketDetails>>> GetTicketDetails()
        {
            var ticketDetails = ridesRepo.GetTicketDetails();
            if (ticketDetails != null)
            {
                return Ok(ticketDetails);
            }
            else
            {
                return NotFound("Cannot Get Ticket Details");
            }

        }



    }
}
