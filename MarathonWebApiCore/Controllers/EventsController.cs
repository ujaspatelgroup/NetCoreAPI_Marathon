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
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<EventsController>
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Events> GetEvents()
        {
            return _context.Event;
        }

        // GET api/<EventsController>/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Events>> GetEventAsync(int id)
        {
            var events = await _context.Event.FindAsync(id);

            if (events == null)
            {
                return NotFound();
            }

            return events;
        }

        // POST api/<EventsController>
        [HttpPost]
        public async Task<ActionResult<Events>> PostEventAsync([FromBody] Events events)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Event.Add(events);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Event successfully!" });

        }

        // PUT api/<EventsController>/5
        [HttpPut]
        public async Task<IActionResult> PutEventAsync(int id, [FromBody] Events events)
        {
            if (id != events.EventId)
            {
                return BadRequest();
            }

            _context.Entry(events).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // DELETE api/<EventsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Events>> DeleteEventAsync(int id)
        {
            var events = await _context.Event.FindAsync(id);
            if (events == null)
            {
                return NotFound();
            }

            _context.Event.Remove(events);
            await _context.SaveChangesAsync();

            return events;
        }

        private bool EventExists(int id)
        {
            return _context.Event.Count(e => e.EventId == id) > 0;
        }
    }
}
