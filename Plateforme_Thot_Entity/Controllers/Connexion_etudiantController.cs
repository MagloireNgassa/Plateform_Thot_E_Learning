using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plateforme_Thot_Entity.Models;
using System.Net;
using System.Net.Mail;
using System.Collections;

namespace Plateforme_Thot_Entity.Controllers
{
    public class Connexion_etudiantController : Controller
    {
        // GET: Connexion_etudiant
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexRetour()//reaffichage de la page apres le click pour inscription au cours
        {
            verifieInscriptionCours();
            returnNiveauScolaire();
            return View("../Accueil/IndexEtudiant");
        }

        [HttpPost]
        public ActionResult Index(string login, string password, string fonction)
        {
            Session["fonction"] = fonction;
            if (Session["fonction"].ToString() == "etudiant") //utilisation de la fonction pour determiner si c'est un etudiant ou un enseignant
            {
                return View(verificationConnection(login, password));//Authentification du compte etudiant
            }
            else
            {
                return View(verificationConnectionEmseignant(login, password));//authetification du compte enseignant
            }

        }

        public ActionResult Changer_login() //action qui appelle la vue changement de login et password
        {

            return View();
        }

        [HttpPost]
        public ActionResult Changer_login(string newlogin, string newpassword) //action qui recupere le new login et new password
        {
            if (VerifieLogin(newlogin))
            {
                ViewBag.Info = "Login deja utilisé!!! veuillez utiliser un autre ";
                return View();
            }
            else
            {
                ChangeLogin(newlogin, newpassword);

                returnNiveauScolaire();  //recuperation du niveau scolaire pour faire le filtre au niveau de l'afficharge
                verifieInscriptionCours();//verification de l'inscription au cours
                return View("../Accueil/IndexEtudiant");  //affiche directement la vue sans paaser par le controller
            }

        }

        //********************************fonction*********************************

        //fonction Authentification de connexion etudiant****************************
        public string verificationConnection(string login, string password)
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                string info;

                try
                {
                    var res = (from e in db.Etudiants
                               where e.Login == login && e.Password == password
                               select e).Single();
                    Session["Id"] = res.EtudiantId;

                    if (res.Password == password && res.Statut == "0")
                    {
                        //c'est la premiere connexion renvoi vers la page de changement du login et password
                        
                        info = "Changer_login";
                        return info;
                    }
                    else
                    {
                        //connexion pour un ancien utilisateur renvoie vers la page d'accueil
                        verifieInscriptionCours(); //verification de l,inscription au cours
                        returnNiveauScolaire();    // recuperation du niveau scolaire
                        info = "../Accueil/IndexEtudiant";
                        return info;
                    }

                }
                catch (Exception)
                {
                    //erreur lors de la connexion
                    ViewBag.log = "Login ou Mot de passe incorect";
                    info = "Index";
                    return info;
                }
            }
        }

        //fonction Authentification de connexion etudiant****************************
        public string verificationConnectionEmseignant(string login, string password)
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                string info;

                try
                {

                    var res = (from e in db.Enseignants
                               where e.Login == login 
                               select e).Single();

                    if (res.Password == password )
                    {
                        //c'est la premiere connexion renvoi vers la page de changement du login et password
                        Session["Id"] = res.EnseignantId;
                        info = "../Accueil/IndexEnseignant";
                        return info;
                    }
                    else
                    {
                        //connexion pour un ancien utilisateur renvoie vers la page d'accueil
                        ViewBag.log = "Login ou Mot de passe incorect";
                        info = "Index";
                        return info;
                    }

                }
                catch (Exception)
                {
                    //erreur lors de la connexion
                    ViewBag.log = "Login ou Mot de passe incorect";
                    info = "Index";
                    return info;
                }
            }
        }

        //fonction update le nouveau login dans la base de donnée*************************
        public void ChangeLogin(string newlogin, string newpassword)
        {
            //recuperation du nouveau login et paasword
            Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2();
            int holdLogin = Convert.ToInt32(Session["Id"]);
            var result = db.Etudiants.Find(holdLogin);
            result.Login = newlogin;
            result.Password = newpassword;
            result.Statut = "1";

            db.SaveChanges();

            //changement du mot de passe effectuer on passe à la page d'acceuil
        }

        public Boolean VerifieLogin(string newLogin)
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                try
                {
                    var res = db.Etudiants.Where(b => b.Login == newLogin).FirstOrDefault();

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

        public void returnNiveauScolaire()
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                int id = Convert.ToInt32(Session["Id"]);
                var res = db.Etudiants.Find(id);

                ViewBag.Niveau_Scolaire = res.Niveau_Scolaire;
            }
        }

        public void verifieInscriptionCours()
        {
            int id = Convert.ToInt32(Session["Id"]);

            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                try
                {
                    //on verifie que etudiantid se trouve dans la table inscription
                    var res = (from b in db.Inscription_Cours
                              where b.EtudiantId == id
                              select b.Cours).ToList();
                     
                    if (res.Count != 0 )
                    {
                        
                        foreach (var element in res)
                        {
                            
                            var r = db.Materiel_Didactiques.Find(element);
                            if (r.Cours.Equals("Asservissement"))
                            {
                                TempData["Asservissement"] = "vous êtes inscrit à ce cours";
                                ViewBag.Asservissement = "hidden";
                            } else{ ViewBag.Asservissement = "button";}

                            if (r.Cours.Equals("Calcul"))
                            {
                                TempData["Calcul"] = "vous êtes inscrit à ce cours";
                                ViewBag.Calcul = "hidden";
                            } else { ViewBag.Calcul = "button";}

                            if (r.Cours.Equals("Electrotechniques"))
                            {
                                TempData["Electrotechnique"] = "vous êtes inscrit à ce cours";
                                ViewBag.Electrotechnique = "hidden";
                            } else { ViewBag.Electrotechnique = "button"; }

                            if (r.Cours.Equals("Histoire geographie"))
                            {
                                TempData["Geographie"] = "vous êtes inscrit à ce cours";
                                ViewBag.Geographie = "hidden";
                            } else { ViewBag.Geographie = "button"; }

                            if (r.Cours.Equals("Moral"))
                            {
                                TempData["Moral"] = "vous êtes inscrit à ce cours";
                                ViewBag.Moral = "hidden";
                            } else { ViewBag.Moral = "button"; }

                            if (r.Cours.Equals("Programmation"))
                            {
                                TempData["Programmation"] = "vous êtes inscrit à ce cours";
                                ViewBag.Programmation = "hidden";
                            } else { ViewBag.Programmation = "button"; }

                            if (r.Cours.Equals("Sciences naturelle"))
                            {
                                TempData["Sciences"] = "vous êtes inscrit à ce cours";
                                ViewBag.Sciences = "hidden";
                            } else { ViewBag.Sciences = "button"; }

                            if (r.Cours.Equals("Mathématique"))
                            {
                                TempData["Mathematiques"] = "vous êtes inscrit à ce cours";
                                ViewBag.Mathematiques = "hidden";
                            } else { ViewBag.Mathematiques = "button"; }

                            if (r.Cours.Equals("Hygiène"))
                            {
                                TempData["Hygiene"] = "vous êtes inscrit à ce cours";
                                ViewBag.Hygiene = "hidden";
                            } else { ViewBag.Hygiene= "button"; }

                        }   
                    }
                    else
                    {
                        ViewBag.Asservissement = "button";
                        ViewBag.Calcul = "button";
                        ViewBag.Electrotechnique = "button";
                        ViewBag.Geographie = "button";
                        ViewBag.Moral = "button";
                        ViewBag.Programmation = "button";
                        ViewBag.Sciences = "button";
                        ViewBag.Hygiene = "button";
                        ViewBag.Mathematiques = "button";
                    }
                }
                catch
                {
                    ViewBag.Asservissement = "button";
                    ViewBag.Calcul = "button";
                    ViewBag.Electrotechnique = "button";
                    ViewBag.Geographie = "button";
                    ViewBag.Moral = "button";
                    ViewBag.Programmation = "button";
                    ViewBag.Sciences = "button";
                    ViewBag.Hygiene = "button";
                    ViewBag.Mathematiques = "button";
                }
            }
        }
    }
}
