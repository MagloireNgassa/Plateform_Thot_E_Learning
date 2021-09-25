using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plateforme_Thot_Entity.Models;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Data.Entity;
using static Plateforme_Thot_Entity.Models.Panier;

namespace Plateforme_Thot_Entity.Controllers
{
    public class AccueilController : Controller
    {
        // GET: Accueil
        public ActionResult IndexEtudiant()
        {
            
            return View();
        }

        public ActionResult IndexEnseignant()
        {
            return View();
        }

        public ActionResult Check(string cour)//action de recuperation d'info apres le click sur le button inscription
        {
            AjoutPanier(cour);

            //Inscription(cour);
            
            return RedirectToAction("../Connexion_etudiant/IndexRetour");
        }

        //fonction pour inscription a un cours
        public void Inscription(string cour)
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                try
                {
                    int id = Convert.ToInt32(Session["Id"]);

                    var b = db.Cours.Where(m=>m.Nom_Cours==cour).FirstOrDefault();
                    
                    db.Inscription_Cours.Add(new Inscription_Cours { EtudiantId=id, CoursId= b.CoursId });
                    db.SaveChanges();
                }
                catch
                { 
                
                }
            }
                
        }

        public void AjoutPanier(string cour)
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                try
                {
                    var b = db.Cours.Where(m => m.Nom_Cours == cour).FirstOrDefault();
                    int UserId = Convert.ToInt32(Session["Id"]);
                    int ProdId = b.CoursId;

                    Panier pan = new Panier(UserId,ProdId);
                    pan.AddProduit();

                   
                   
                }
                catch(Exception ex)
                {

                }
            }

        }



    }
}