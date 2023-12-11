using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP3.Models.DataManager;
using TP3.Models.EntityFramework;
using TP3.Models.Repository;

namespace TP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        // private readonly NotationDbContext _context;
        private readonly IDataRepository<Utilisateur> dataRepository;
        public UtilisateursController(IDataRepository<Utilisateur> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Utilisateurs
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Utilisateur>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            //if (_context.Utilisateurs == null) return NotFound("La liste des utilisateurs est inaccessible.");
            //return await _context.Utilisateurs.ToListAsync();

            return dataRepository.GetAll();
        }

        // GET: api/Utilisateurs/5
        [HttpGet]
        [ActionName("GetById")]
        [Route("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurById(int id)
        {
            //if (_context.Utilisateurs == null) return NotFound("La liste des utilisateurs est inaccessible.");
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            //if (utilisateur == null) return NotFound("Utilisateur introuvable.");
            //return utilisateur;

            var utilisateur = dataRepository.GetById(id);
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
            //if (_context.Utilisateurs == null) return NotFound("La liste des utilisateurs est inaccessible."); 
            //var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(x => x.Mail == email);
            //if (utilisateur == null) return NotFound("Utilisateur introuvable.");
            //return utilisateur;

            var utilisateur = dataRepository.GetByString(email);
            if (utilisateur == null) return NotFound("Utilisateur introuvable.");
            return utilisateur;
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            //if (_context.Utilisateurs == null) return NotFound("La liste des utilisateurs est inaccessible.");
            //if (id != utilisateur.UtilisateurId) return BadRequest("Utilisateur introuvable.");
            //_context.Entry(utilisateur).State = EntityState.Modified;
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UtilisateurExists(id)) return BadRequest("Utilisateur introuvable.");
            //    else throw;
            //}
            //return NoContent();

            if (id != utilisateur.UtilisateurId) return BadRequest("Utilisateur introuvable.");
            var userToUpdate = dataRepository.GetById(id);

            if (userToUpdate == null) return NotFound("Utilisateur introuvable.");
            else
            {
                dataRepository.Update(userToUpdate.Value, utilisateur);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            //if (_context.Utilisateurs == null) return NotFound("La liste des utilisateurs est inaccessible.");
            //_context.Utilisateurs.Add(utilisateur);
            //await _context.SaveChangesAsync();
            //var result = CreatedAtAction("GetUtilisateur", new { id = utilisateur.UtilisateurId }, utilisateur);
            //if (result == null) return BadRequest("Impossible d'ajouter ce nouvel utilisateur");
            //return result;

            if (!ModelState.IsValid) return BadRequest(ModelState);
            dataRepository.Add(utilisateur);
            return CreatedAtAction("GetById", new { id = utilisateur.UtilisateurId }, utilisateur); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            //if (_context.Utilisateurs == null)
            //{
            //    return NotFound("La liste des utilisateurs est vide.");
            //}
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            //if (utilisateur == null)
            //{
            //    return NotFound("Utilisateur introuvable.");
            //}
            //_context.Utilisateurs.Remove(utilisateur);
            //await _context.SaveChangesAsync();
            //return NoContent();

            var utilisateur = dataRepository.GetById(id);
            if (utilisateur == null) return NotFound("Utilisateur introuvable.");
            dataRepository.Delete(utilisateur.Value);
            return NoContent();
        }

        /*
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Utilisateur>> PatchUtilisateur(int id, [FromBody] JsonPatchDocument<Utilisateur> patchUser)
        {
            ////if (_context.Utilisateurs == null) return NotFound("La liste des utilisateurs est inaccessible.");
            ////var entity = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.UtilisateurId == id);
            ////if (entity == null) return BadRequest("Utilisateur introuvable.");
            ////patchUser.ApplyTo(entity);
            ////return entity;
        }
        */

        //private bool UtilisateurExists(int id)
        //{
        //    return (_context.Utilisateurs?.Any(e => e.UtilisateurId == id)).GetValueOrDefault();
        //}
    }
}
