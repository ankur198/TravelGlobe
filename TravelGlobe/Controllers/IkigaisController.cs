using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGlobe.Model;
using TravelGlobe.Models;

namespace TravelGlobe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IkigaisController : ControllerBase
    {
        private readonly TravelGlobeContext _context;

        public IkigaisController(TravelGlobeContext context)
        {
            _context = context;
        }

        // GET: api/Ikigais
        [HttpGet]
        [EnableQuery()]
        public async Task<ActionResult<IEnumerable<Ikigai>>> GetIkigai()
        {

            return await _context.Ikigai.ToListAsync();
        }

        // GET: api/Ikigais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ikigai>> GetIkigai(int id)
        {
            var ikigai = await _context.Ikigai.FindAsync(id);

            if (ikigai == null)
            {
                return NotFound();
            }

            return ikigai;
        }

        // PUT: api/Ikigais/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIkigai(int id, Ikigai ikigai)
        {
            if (id != ikigai.ID)
            {
                return BadRequest();
            }

            _context.Entry(ikigai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IkigaiExists(id))
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

        // POST: api/Ikigais
        [HttpPost]
        public async Task<ActionResult<Ikigai>> PostIkigai(Ikigai ikigai)
        {
            ikigai.Positions = new List<Position>();

            _context.Ikigai.Add(ikigai);

            await _context.SaveChangesAsync();

            foreach (var position in Enum.GetNames(typeof(Positions)))
            {
                ikigai.Positions.Add(new Position { position = (Positions)Enum.Parse(typeof(Positions), position), Ikigai_ID = ikigai.ID });
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIkigai", new { id = ikigai.ID }, ikigai);
        }

        // DELETE: api/Ikigais/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ikigai>> DeleteIkigai(int id)
        {
            var ikigai = await _context.Ikigai.FindAsync(id);
            if (ikigai == null)
            {
                return NotFound();
            }

            _context.Ikigai.Remove(ikigai);
            await _context.SaveChangesAsync();

            return ikigai;
        }

        private bool IkigaiExists(int id)
        {
            return _context.Ikigai.Any(e => e.ID == id);
        }
    }
}
