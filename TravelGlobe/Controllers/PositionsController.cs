using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGlobe.Model;
using TravelGlobe.Models;

namespace TravelGlobe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly TravelGlobeContext _context;

        public PositionsController(TravelGlobeContext context)
        {
            _context = context;
        }

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            return await _context.Positions.ToListAsync();
        }

        // id is Ikigai id
        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Position>>> GetPosition(int id)
        {
            var ikigai = await _context.Ikigai.FindAsync(id);

            if (ikigai == null)
            {
                return NotFound();
            }

            var positions = ikigai.Positions;

            return positions;
        }

        // GET: api/Positions/5
        [HttpGet("{id}/{pos}")]
        public async Task<ActionResult<Position>> GetPositionEnum(int id, string pos)
        {
            var position = await GetPositionList(id, pos);
            return position;
        }

        // PUT: api/Positions/5
        [HttpPut("{id}/{pos}/{activityId}")]
        public async Task<IActionResult> PutPosition(int id, string pos, int activityId, Activity activity)
        {
            if (activityId != activity.ID)
            {
                return BadRequest();
            }

            _context.Entry(activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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

        // POST: api/Positions
        [HttpPost("{id}/{pos}")]
        public async Task<ActionResult<Position>> PostPosition(int id, string pos, Activity activity)
        {

            var position = await GetPositionList(id, pos);

            position.Activities.Add(activity);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosition", new { id = id, pos = pos }, position);
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}/{pos}/{activityId}")]
        public async Task<ActionResult<Activity>> DeletePosition(int id,string pos, int activityId)
        {
            var activity = await _context.Activity.FindAsync(activityId);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activity.Remove(activity);
            await _context.SaveChangesAsync();

            return activity;
        }

        private bool PositionExists(int id)
        {
            return _context.Positions.Any(e => e.ID == id);
        }

        private async Task<Position> GetPositionList(int IkigaiId, string position)
        {
            var ikigai = await _context.Ikigai.FindAsync(IkigaiId);

            if (ikigai == null)
            {
                return null;
            }

            Positions positions;

            if (!Enum.TryParse<Positions>(position, true, out positions))
            {
                return null;
            }
            return ikigai.Positions.FirstOrDefault(x => x.position == positions);
        }
    }
}
