using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KrushiWebAPI.Context;
using KrushiWebAPI.Models;

namespace KrushiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly ProjectDBContext _context;

        public RecommendationsController(ProjectDBContext context)
        {
            _context = context;
        }

        // GET: api/Recommendations
        [HttpGet("{farmerid}")]
        public async Task<ActionResult<IEnumerable<Recommendation>>> GetRecommendations(int farmerid)
        {
          if (_context.Recommendations == null)
          {
              return NotFound();
          }
            return await _context.Recommendations.Include(a => a.Admin).Include(c => c.Crop).Include(f => f.Farmer).Where(r=>r.Farmerid == farmerid).ToListAsync();

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recommendation>>> GetRecommendations()
        {

            var list = _context.Recommendations
                  .GroupBy(p => p.Rdate)
                  .Select(g => new { rdate = g.Key, count = g.Count() }).ToList();
            return Ok(list);
        }

        // GET: api/Recommendations/1/5


        [HttpGet("{farmerid}/{id}")]
        public async Task<ActionResult<Recommendation>> GetRecommendation(int id)
        {
          if (_context.Recommendations == null)
          {
              return NotFound();
          }
            var recommendation = await _context.Recommendations.FindAsync(id);

            if (recommendation == null)
            {
                return NotFound();
            }

            return recommendation;
        }

        // PUT: api/Recommendations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecommendation(int id, Recommendation recommendation)
        {
            if (id != recommendation.Id)
            {
                return BadRequest();
            }

            _context.Entry(recommendation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecommendationExists(id))
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

        // POST: api/Recommendations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recommendation>> PostRecommendation(Recommendation recommendation)
        {
          if (_context.Recommendations == null)
          {
              return Problem("Entity set 'ProjectDBContext.Recommendations'  is null.");
          }
            _context.Recommendations.Add(recommendation);
            await _context.SaveChangesAsync();

            return Ok(recommendation);
        }

        // DELETE: api/Recommendations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecommendation(int id)
        {
            if (_context.Recommendations == null)
            {
                return NotFound();
            }
            var recommendation = await _context.Recommendations.FindAsync(id);
            if (recommendation == null)
            {
                return NotFound();
            }

            _context.Recommendations.Remove(recommendation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecommendationExists(int id)
        {
            return (_context.Recommendations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
