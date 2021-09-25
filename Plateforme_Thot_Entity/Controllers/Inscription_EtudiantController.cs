using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plateforme_Thot_Entity.Models;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Data.SqlClient;
using static Plateforme_Thot_Entity.Models.CollecteItems;

namespace Plateforme_Thot_Entity.Controllers
{
    public class Inscription_EtudiantController : Controller
    {
        private Plateforme_Thot_Data_2 db;

        // GET: Inscription_Etudiant
        public ActionResult Index()
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                try
                {
                    /*db.Cours.Add(new Cours { Niveau_Scolaire="Primaire",Nom_Cours="Calcul",Image="Calcul.jpg",Description="Effectue les additions simple",Prix=350});
                    db.Cours.Add(new Cours { Niveau_Scolaire = "Primaire", Nom_Cours = "Moral", Image = "Morale.jpg", Description = "Apprendre à être polie", Prix = 350 });
                    db.Cours.Add(new Cours { Niveau_Scolaire = "Primaire", Nom_Cours = "Hygiène", Image = "Hygiène.jpg", Description = "Apprendre à rester propre", Prix = 350 });
                    db.Cours.Add(new Cours { Niveau_Scolaire = "Secondaire", Nom_Cours = "Mathématiques",Image="Mathématique.jpg",Description="Calcul des fonctions algorithmiques",Prix=450 });
                    db.Cours.Add(new Cours { Niveau_Scolaire = "Secondaire", Nom_Cours = "Sciences", Image = "Sciences.jpg", Description = "Apprendre des organismes vivant", Prix = 450 });
                    db.Cours.Add(new Cours { Niveau_Scolaire = "Secondaire", Nom_Cours = "Histoire", Image = "Histoire.jpg", Description = "Connaitre sont passée et son environnement", Prix = 450 });
                    db.Cours.Add(new Cours { Niveau_Scolaire = "Universitaire", Nom_Cours = "Asservissement",Image="Asservissement.jpg",Description="Application des fonctions de transferts",Prix=550 });
                    db.Cours.Add(new Cours { Niveau_Scolaire = "Universitaire", Nom_Cours = "Programmation", Image = "Programmation.jpg", Description = "Apprendre à devellopper dekstop et mobile", Prix = 550 });
                    db.Cours.Add(new Cours { Niveau_Scolaire = "Universitaire", Nom_Cours = "Electrotechnique", Image = "Electrotechnique.jpg", Description = "Commande et application industrielle", Prix = 550 });
                   /* db.SaveChanges();*/
                     /*db.Etudiants.Add(new Etudiant { Login = "mag_ndosseu@outlook.com", Password = "Magloire123", Nom = "Ngassa", Prenom = "Magloire", Niveau_Scolaire = "Universitaire", Email = "mag_ndosseu@outlook.com", Statut = "0" });
                     db.Etudiants.Add(new Etudiant { Login = "magloire", Password = "magloire123", Nom = "Sagenor", Prenom = "Durant", Niveau_Scolaire = "Universitaire", Email = "mag_ndosseu@yahoo.fr", Statut = "1" });
                     db.Etudiants.Add(new Etudiant { Login = "MabelNdosseu", Password = "mabel123", Nom = "Sagenor", Prenom = "Durant", Niveau_Scolaire = "Secondaire", Email = "mag_ndosseu@yahoo.fr", Statut = "1" });
                     db.Etudiants.Add(new Etudiant { Login = "nananaomie", Password = "nananaomie", Nom = "Sagenor", Prenom = "Durant", Niveau_Scolaire = "Primaire", Email = "mag_ndosseu@yahoo.com", Statut = "1" });
                     db.SaveChanges();
                     /*db.Enseignants.Add(new Enseignant {Login = "marcgrenier", Password = "Marc123", Nom = "Grenier", Prenom = "Marc", Specialisation = "Programmation", Email = "tchuik_ndosseu@yahoo.fr" });
                     db.Enseignants.Add(new Enseignant {Login = "fernandtonye", Password="Fernand123", Nom = "Tonye", Prenom = "Fernand", Specialisation = "industriel", Email = "mag_ndosseu@yahoo.fr"});
                     db.Enseignants.Add(new Enseignant { Login = "magloirengassa", Password = "magloire123", Nom = "Ngassa", Prenom = "Magloire", Specialisation = "maintenance", Email = "mag_ndosseu@outlook.com" });
                     db.Enseignants.Add(new Enseignant { Login = "magloireplata", Password = "plataman", Nom = "", Prenom = "Magloire", Specialisation = "maintenance", Email = "mag_ndosseu@outlook.com" });*/


                    /*db.Materiel_Didactiques.Add(new Materiel_Didactiques { Cours = "Calcul" });
                    db.Materiel_Didactiques.Add(new Materiel_Didactiques { Cours = "Hygiène" });
                    db.Materiel_Didactiques.Add(new Materiel_Didactiques { Cours = "Morale" });
                    db.Materiel_Didactiques.Add(new Materiel_Didactiques { Cours = "Mathématique" });
                    db.Materiel_Didactiques.Add(new Materiel_Didactiques { Cours = "Sciences naturelle" });
                    db.Materiel_Didactiques.Add(new Materiel_Didactiques { Cours = "Histoire geographie" });
                    db.Materiel_Didactiques.Add(new Materiel_Didactiques { Cours = "Programmation" });
                    db.Materiel_Didactiques.Add(new Materiel_Didactiques { Cours = "Asservissement" });
                    db.Materiel_Didactiques.Add(new Materiel_Didactiques { Cours = "Electrotechnique" });*/

                    /* db.SaveChanges();*/

                    /* 
                     db.SaveChanges();*/

                    var users = db.Cours.ToList();

                     

                   // ViewBag.selectcours = new SelectList(viewModel.Cours);

                    return View();
                }
                catch (Exception ex)
                {
                    return View();
                }
                
            }
            
        }
        [HttpPost]
        public ActionResult Index(Etudiant etudiant, listeNiveauScolaire Item)
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                try
                {
                   etudiant.Niveau_Scolaire = Item.ToString();
                    etudiant.setLogin();
                    etudiant.setPassword();
                    etudiant.setStatut();
                    db.Etudiants.Add(etudiant);

                    if (VerifieEmail(etudiant.Email))
                    {
                        ViewBag.selectinfo = "Un Etudiant est déjà inscrit avec cet Email";
                        return View();
                    }
                    else 
                    {
                       
                        db.SaveChanges();
                    }

                    if (etudiant.sendMail(etudiant.constEmail()))
                    {
                        ViewBag.selectinfo = "Enregistrement reussi Bienvenu " + etudiant.Prenom;
                        ViewBag.selectinfo1 = "Vérifiez votre Email pour avoir votre Login et Password provisoir";

                    }
                    else
                    {
                        ViewBag.selectinfo = "Problème de connection essayez plutart";
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.selectinfo = "Problème de connection essayez plutart";
                }

            }
            return View();
        }

        public Boolean VerifieEmail(string email)
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                try
                {
                    var res = db.Etudiants.Where(b => b.Email == email).FirstOrDefault();

                    if (res != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

    }
}