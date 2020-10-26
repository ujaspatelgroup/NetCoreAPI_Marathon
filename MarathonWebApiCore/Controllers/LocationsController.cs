using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarathonWebApiCore.Helpers;
using MarathonWebApiCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace MarathonWebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Management")]
    public class LocationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        
        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<LocationsController>
        [Authorize]
        [HttpGet]
        public IEnumerable<Location> GetLocations()
        {
            return _context.Location;
        }

        // GET api/<LocationsController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocationAsync(int id)
        {
            var location = await _context.Location.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        // POST api/<LocationsController>
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocationAsync([FromBody] Location location)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Location.Add(location);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Location add successfully!" });

        }

        // PUT api/<LocationsController>/5
        [HttpPut]
        public async Task<IActionResult> PutLocationAsync(int id, [FromBody] Location location)
        {
            if (id != location.LocationId)
            {
                return BadRequest();
            }

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<LocationsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Location>> DeleteLocationAsync(int id)
        {
            var location = await _context.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Location.Remove(location);
            await _context.SaveChangesAsync();

            return location;
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Count(e => e.LocationId == id) > 0;
        }
    }
}
