using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarathonWebApiCore.Helpers;
using MarathonWebApiCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarathonWebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegistrationsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public RegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<RegistrationsController>
        [Authorize(Policy = "Management")]
        [HttpGet]
        public IEnumerable<Registration> GetRegistration()
        {
            return _context.Registration;
        }

        // GET api/<RegistrationsController>/5
        [Authorize(Policy = "Management")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Registration>> GetRegistrationAsync(int id)
        {
            var registration = await _context.Registration.FindAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            return registration;
        }

        // POST api/<RegistrationsController>
        [HttpPost]
        public async Task<ActionResult<IActionResult>> PostRegistrationAsync([FromBody] Registration registration)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Registration.Add(registration);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Registration successfully!" });

        }

        // PUT api/<RegistrationsController>/5
        [HttpPut]
        public async Task<IActionResult> PutRegistrationAsync(int id, [FromBody] Registration registration)
        {
            if (id != registration.RegistarionId)
            {
                return BadRequest();
            }

            _context.Entry(registration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationExists(id))
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

        // DELETE api/<RegistrationsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Registration>> DeleteRegistrationAsync(int id)
        {
            var registration = await _context.Registration.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            _context.Registration.Remove(registration);
            await _context.SaveChangesAsync();

            return registration;
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registration.Count(e => e.RegistarionId == id) > 0;
        }
    }
}
