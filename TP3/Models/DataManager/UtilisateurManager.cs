using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP3.Models.EntityFramework;
using TP3.Models.Repository;

namespace TP3.Models.DataManager
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
        public ActionResult<Utilisateur> GetById(int id)
        {
            return notationDbContext.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == id);
        }
        public ActionResult<Utilisateur> GetByString(string mail)
        {
            return notationDbContext.Utilisateurs.FirstOrDefault(u => u.Mail.ToUpper() == mail.ToUpper());
        }
        public void Add(Utilisateur entity)
        {
            notationDbContext.Utilisateurs.Add(entity);
            notationDbContext.SaveChanges();
        }
        public void Update(Utilisateur utilisateur, Utilisateur entity)
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
            notationDbContext.SaveChanges();
        }
        public void Delete(Utilisateur utilisateur)
        {
            notationDbContext.Utilisateurs.Remove(utilisateur);
            notationDbContext.SaveChanges();
        }
    }
}