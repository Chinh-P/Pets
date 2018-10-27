using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Check2.Models;
using TestCore.Model;

//[assembly:ApiConventionType(typeof(DefaultApiConvention))]
namespace Check2.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly CheckContext _context;

        public PetsController(CheckContext context)
        {
            _context = context;
        }

        // GET: api/Pets
        [HttpGet]
        public IEnumerable<Pets> GetPets()
        {
            return _context.Pets;
        }

        // GET: api/Pets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPets([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pets = await _context.Pets.FindAsync(id);

            if (pets == null)
            {
                return NotFound();
            }

            return Ok(pets);
        }

        // PUT: api/Pets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPets([FromRoute] int id, [FromBody] Pets pets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pets.Id)
            {
                return BadRequest();
            }

            _context.Entry(pets).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetsExists(id))
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

        // POST: api/Pets
        [HttpPost]
        public async Task<IActionResult> PostPets([FromBody] Pets pets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pets.Add(pets);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPets", new { id = pets.Id }, pets);
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePets([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pets = await _context.Pets.FindAsync(id);
            if (pets == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pets);
            await _context.SaveChangesAsync();

            return Ok(pets);
        }

        private bool PetsExists(int id)
        {
            return _context.Pets.Any(e => e.Id == id);
        }
    }
}