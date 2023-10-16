using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using TP3.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TP3.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        [TestMethod()]
        public void GetUtilisateursTest()
        {
            var builder = new DbContextOptionsBuilder<NotationDbContext>().UseNpgsql("Server=localhost;port=5432;Database=TP3; uid=root;password=root;");
            NotationDbContext _context = new NotationDbContext(builder.Options);
            
            var users = _context.Utilisateurs.ToList();
            Assert.IsTrue(users.Count > 0 && users.GetType() == typeof(List<Utilisateur>));
        }

        [TestMethod()]
        public void GetUtilisateurByIdPassTest()
        {
            var builder = new DbContextOptionsBuilder<NotationDbContext>().UseNpgsql("Server=localhost;port=5432;Database=TP3; uid=root;password=root;");
            NotationDbContext _context = new NotationDbContext(builder.Options);

            var user = _context.Utilisateurs.Find(1);
            Assert.IsTrue(user != null && user.GetType() == typeof(Utilisateur));
        }

        [TestMethod()]
        public void GetUtilisateurByIdFailTest()
        {
            var builder = new DbContextOptionsBuilder<NotationDbContext>().UseNpgsql("Server=localhost;port=5432;Database=TP3; uid=root;password=root;");
            NotationDbContext _context = new NotationDbContext(builder.Options);

            var user = _context.Utilisateurs.Find(0);
            Assert.IsTrue(user == null);
        }

        [TestMethod()]
        public void GetUtilisateurByEmailPassTest()
        {
            var builder = new DbContextOptionsBuilder<NotationDbContext>().UseNpgsql("Server=localhost;port=5432;Database=TP3; uid=root;password=root;");
            NotationDbContext _context = new NotationDbContext(builder.Options);

            var user = _context.Utilisateurs.Where(u => u.Mail == "lspongv@berkeley.edu")
                .FirstOrDefault();
            Assert.IsTrue(user != null && user.GetType() == typeof(Utilisateur));
        }

        [TestMethod()]
        public void GetUtilisateurByEmailFailTest()
        {
            var builder = new DbContextOptionsBuilder<NotationDbContext>().UseNpgsql("Server=localhost;port=5432;Database=TP3; uid=root;password=root;");
            NotationDbContext _context = new NotationDbContext(builder.Options);

            var user = _context.Utilisateurs.Where(u => u.Mail == "apjebvpkqhdfbvkjbvpksjdfbvlkshdfbv");
            Assert.IsTrue(user.Count() == 0);
        }

        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK()
        {
            var builder = new DbContextOptionsBuilder<NotationDbContext>().UseNpgsql("Server=localhost;port=5432;Database=TP3; uid=root;password=root;Include Error Detail");
            NotationDbContext _context = new NotationDbContext(builder.Options);
            var _controller = new UtilisateursController(_context);
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
             Utilisateur userAtester = new Utilisateur()
             {
                 Nom = "MACHIN",
                 Prenom = "Luc",
                 Mobile = "0606070809",
                 Mail = "machin" + chiffre + "@gmail.com",
                 Pwd = "Toto1234!",
                 Rue = "Chemin de Bellevue",
                 CodePostal = "74940",
                 Ville = "Annecy-le-Vieux",
                 Pays = "France",
                 Latitude = null,
                 Longitude = null
             };
            var result = _controller.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de manière
             Utilisateur? userRecupere = _context.Utilisateurs.Where(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper()).FirstOrDefault();
             userAtester.UtilisateurId = userRecupere.UtilisateurId;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }

        //test the PUT
        [TestMethod]
        public void PutUtilisateurTest()
        {
            var builder = new DbContextOptionsBuilder<NotationDbContext>().UseNpgsql("Server=localhost;port=5432;Database=TP3; uid=root;password=root;");
            NotationDbContext _context = new NotationDbContext(builder.Options);
            var _controller = new UtilisateursController(_context);
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN2",
                Prenom = "Luc",
                Mobile = "606070809",
                Mail = "machin2" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            var result = _controller.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de manière
            Utilisateur? userRecupere = _context.Utilisateurs.Where(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper()).FirstOrDefault();
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
            userAtester.Nom = "MACHIN2";
            userAtester.Prenom = "Luc2";
            userAtester.Mobile = "0606070809";
            userAtester.Mail = "machin2" + chiffre + "@gmail.com";

            //put
            var result2 = _controller.PutUtilisateur(userAtester.UtilisateurId, userAtester).Result;
            Utilisateur? userRecupere2 = _context.Utilisateurs.Where(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper()).FirstOrDefault();
            Assert.AreEqual(userRecupere2, userAtester, "Utilisateurs pas identiques");
        }
    }
}