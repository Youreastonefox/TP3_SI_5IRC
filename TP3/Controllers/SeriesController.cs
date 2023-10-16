using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP3.Models.EntityFramework;

namespace TP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly NotationDbContext _context;

        public SeriesController(NotationDbContext context)
        {
            _context = context;
        }

        // GET: api/Series
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Serie>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Serie>>> GetSeries()
        {
            if (_context.Series == null) return NotFound("La liste des séries est inaccessible.");
            return await _context.Series.ToListAsync();
        }

        // GET: api/Series/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Serie))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Serie>> GetSerie(int id)
        {
            if (_context.Series == null) return NotFound("La liste des séries est inaccessible.");

            var serie = await _context.Series.FindAsync(id);

            if (serie == null) return NotFound("Série introuvable");

            return serie;
        }

        // PUT: api/Series/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutSerie(int id, Serie serie)
        {
            if (_context.Series == null) return NotFound("La liste des séries est inaccessible.");

            if (id != serie.SerieId) return BadRequest("Série introuvable.");

            _context.Entry(serie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SerieExists(id))
                {
                    BadRequest("Série introuvable.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Series
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Serie>> PostSerie(Serie serie)
        {
            if (_context.Series == null) return NotFound("La liste des séries est inaccessible.");

            _context.Series.Add(serie);
            await _context.SaveChangesAsync();

            var result = CreatedAtAction("GetSerie", new { id = serie.SerieId }, serie)
            if (result == null) return BadRequest("Impossible d'ajouter ce nouvel utilisateur");

            return result;
        }

        // DELETE: api/Series/5
        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSerie(int id)
        {
            if (_context.Series == null)
            {
                return NotFound();
            }
            var serie = await _context.Series.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }

            _context.Series.Remove(serie);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */

        private bool SerieExists(int id)
        {
            return (_context.Series?.Any(e => e.SerieId == id)).GetValueOrDefault();
        }
    }
}
