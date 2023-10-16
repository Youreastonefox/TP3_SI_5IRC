using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP3.Models.EntityFramework;

namespace TP3.Models
{
    public class UtilisateurManager : IDataRepository<Utilisateur>
    {
        readonly NotationDbContext? notationDbContext;
        public UtilisateurManager() { }
        public UtilisateurManager(NotationDbContext context)
        {
            notationDbContext = context;
        }

        public ActionResult<IEnumerable<Utilisateur>> GetAll()
        {
            return notationDbContext.Utilisateurs.ToList();
        }

        public async Task<ActionResult<Utilisateur>> GetByIdAsync(int id)
        {
            return await notationDbContext.Utilisateurs.FirstOrDefaultAsync(u => u.UtilisateurId == id);
        }

        public async Task<ActionResult<Utilisateur>> GetByStringAsync(string mail)
        {
            return await notationDbContext.Utilisateurs.FirstOrDefaultAsync(u => u.Mail.ToUpper() == mail.ToUpper());
        }

        public async Task AddAsync(Utilisateur entity)
        {
            await notationDbContext.Utilisateurs.AddAsync(entity);
            await notationDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Utilisateur utilisateur, Utilisateur entity)
        {
            notationDbContext.Entry(utilisateur).State = EntityState.Modified;
            utilisateur.UtilisateurId = entity.UtilisateurId;
            utilisateur.Nom = entity.Nom;
            utilisateur.Prenom = entity.Prenom;
            utilisateur.Mail = entity.Mail;
            utilisateur.Rue = entity.Rue;
            utilisateur.CodePostal = entity.CodePostal;
            utilisateur.Ville = entity.Ville;
            utilisateur.Pays = entity.Pays;
            utilisateur.Latitude = entity.Latitude;
            utilisateur.Longitude = entity.Longitude;
            utilisateur.Pwd = entity.Pwd;
            utilisateur.Mobile = entity.Mobile;
            utilisateur.NotesUtilisateur = entity.NotesUtilisateur;
            await notationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Utilisateur utilisateur)
        {
            notationDbContext.Utilisateurs.Remove(utilisateur);
            await notationDbContext.SaveChangesAsync();
        }
    }
}
