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
    public class NotationsController : ControllerBase
    {
        private readonly NotationDbContext _context;

        public NotationsController(NotationDbContext context)
        {
            _context = context;
        }

        // GET: api/Notations
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Notation>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Notation>>> GetNotations()
        {
            if (_context.Notations == null) return NotFound("La liste des notations est vide ou inaccessible.");
            return await _context.Notations.ToListAsync();
        }

        // GET: api/Notations/GetById/5
        [HttpGet]
        [ActionName("GetById")]
        [Route("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Notation))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Notation>> GetNotationById(int id)
        {
            if (_context.Notations == null) return NotFound("La liste des notations est vide ou inaccessible.");

            var notation = await _context.Notations.FindAsync(id);

            if (notation == null) return BadRequest("Notation introuvable.");

            return notation;
        }

        // PUT: api/Notations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutNotation(int id, Notation notation)
        {
            if (_context.Notations == null) return NotFound("La liste des notations est vide ou inaccessible.");

            if (id != notation.UtilisateurId) return BadRequest("Notation introuvable.");

            _context.Entry(notation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotationExists(id))
                {
                    return BadRequest("Notation introuvable");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Notations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Notation>> PostNotation(Notation notation)
        {
            if (_context.Notations == null) return NotFound("La liste des notations est vide ou inaccessible.");

            _context.Notations.Add(notation);
            await _context.SaveChangesAsync();

            var result = CreatedAtAction("GetNotation", new { id = notation.UtilisateurId }, notation);
            if (result == null) return BadRequest("Impossible d'ajouter ce nouvel utilisateur");

            return result;
        }

        // DELETE: api/Notations/5
        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotation(int id)
        {
            if (_context.Notations == null)
            {
                return NotFound();
            }
            var notation = await _context.Notations.FindAsync(id);
            if (notation == null)
            {
                return NotFound();
            }

            _context.Notations.Remove(notation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */

        private bool NotationExists(int id)
        {
            return (_context.Notations?.Any(e => e.UtilisateurId == id)).GetValueOrDefault();
        }
    }
}
