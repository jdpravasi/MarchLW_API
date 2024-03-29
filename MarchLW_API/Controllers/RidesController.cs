using MarchLW_API.Models;
using MarchLW_API.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace MarchLW_API.Controllers
{

    [Route("api/Rides")]
    [ApiController]
    public class RidesController : Controller
    {
        private readonly IRides<Rides> ridesRepo;
        public RidesController(IRides<Rides> rides)
        {
            ridesRepo = rides;
        }
        [HttpPost("Create", Name = "CreateRide")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(Rides obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }
            ridesRepo.Add(obj);
            ridesRepo.Save();
            return Created("GetAllRides", obj);
        }

        [HttpPut("Update", Name = "UpdateRide")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(Rides obj)
        {
            if (obj.ID == null)
            {
                return BadRequest();
            }
            var ride = ridesRepo.GetRide(obj.ID).FirstOrDefault();
            ridesRepo.Update(obj);
            ridesRepo.Save();
            return Ok(obj);
        }

        [HttpDelete("Delete/{id:int}", Name = "DeleteRide")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var ride = ridesRepo.GetRide(id).FirstOrDefault();
            ridesRepo.Remove(ride);
            ridesRepo.Save();
            return NoContent();
        }

        [HttpGet("GetRide/{id:int}", Name = "GetRide")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Rides>> GetRide(int id)
        {
            var ride = ridesRepo.GetRide(id).FirstOrDefault();

            if (ride != null)
            {
                return Ok(ride);
            }
            else
            {
                return NotFound("Ride not found");
            }
        }
    }
}
