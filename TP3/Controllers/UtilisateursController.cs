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
    public class UtilisateursController : ControllerBase
    {
        private readonly NotationDbContext _context;

        public UtilisateursController(NotationDbContext context)
        {
            _context = context;
        }

        // GET: api/Utilisateurs
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Utilisateur>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            if (_context.Utilisateurs == null)
            {
                return NotFound("La liste des utilisateurs est vide.");
            }

            return await _context.Utilisateurs.ToListAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet]
        [ActionName("GetById")]
        [Route("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurById(int id)
        {
            if (_context.Utilisateurs == null) return NotFound("La liste des utilisateurs est vide.");

            var utilisateur = await _context.Utilisateurs.FindAsync(id);

            if (utilisateur == null) return NotFound("Utilisateur introuvable.");

            return utilisateur;
        }

        // GET: api/Utilisateurs/mail@gmail.com
        [HttpGet]
        [ActionName("GetByEmail")]
        [Route("[action]/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurByEmail(string email)
        {
            if (_context.Utilisateurs == null) return NotFound("La liste des utilisateurs est vide."); 

            var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(x => x.Mail == email);

            if (utilisateur == null) return NotFound("Utilisateur introuvable.");

            return utilisateur;
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.UtilisateurId)
            {
                return BadRequest("Utilisateur avec cet id introuvable.");
            }

            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
                {
                    return NotFound("Utilisateur introuvable.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            if (_context.Utilisateurs == null) return NotFound("La liste des utilisateurs est vide.");

            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            var result = CreatedAtAction("GetUtilisateur", new { id = utilisateur.UtilisateurId }, utilisateur);
            if (result == null) return BadRequest("Impossible d'ajouter ce nouvel utilisateur");

            return result;
        }

        // DELETE: api/Utilisateurs/5
        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            if (_context.Utilisateurs == null)
            {
                return NotFound("La liste des utilisateurs est vide.");
            }
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound("Utilisateur introuvable.");
            }

            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */

        private bool UtilisateurExists(int id)
        {
            return (_context.Utilisateurs?.Any(e => e.UtilisateurId == id)).GetValueOrDefault();
        }

    }
}
