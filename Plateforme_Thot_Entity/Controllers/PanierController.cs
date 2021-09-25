using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plateforme_Thot_Entity.Models;
using static Plateforme_Thot_Entity.Models.Panier;
using Serilog;

namespace Plateforme_Thot_Entity.Controllers
{
    public class PanierController : Controller
    {
        // GET: Panier
        public ActionResult Index()
        {
            int id_user = Convert.ToInt32(Session["Id"]);
            
            return View(AffichePanier(id_user));
        }

 

        public ActionResult Update(string stat, int id_cour)
        {
            int id_user = Convert.ToInt32(Session["Id"]);
            UpdateQuantite(id_user, id_cour, stat);
            

           //ViewBag.verifie = "sa marche";
            return View("Index", AffichePanier(id_user));
        }


        public List<Affiche> AffichePanier(int id_user)
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                try
                {
                    /* var query = (from u in db.Paniers
                                  where u.EtudiantId == id_user
                                  join s in db.Cours
                                  on u.CoursId equals s.CoursId
                                  select new 
                                  {
                                      imag = s.Image,
                                      nom = s.Nom_Cours,
                                      quantit = u.Quantite,
                                      id = s.CoursId
                                  })//new anonymous object is possible in linq2entities
                         .ToList()
                         .Select(x => new Affiche(x.imag, x.nom, x.quantit,x.id));

                     return (Affiche)query;*/

                    var query = (from u in db.Paniers
                                 where u.EtudiantId == id_user
                                 join s in db.Cours
                                 on u.CoursId equals s.CoursId
                                 select new Affiche
                                 {
                                     Images = s.Image,
                                     Nom = s.Nom_Cours,
                                     Quantite = u.Quantite,
                                     Id_Produit = s.CoursId
                                 })//new anonymous object is possible in linq2entities
                       .ToList();

                   /* List<Affiche> tab = new List<Affiche>();
                    foreach (Affiche elemrnt in query)
                    {
                        Affiche temps = new Affiche();
                        temps = elemrnt;
                        tab.Add(temps);
                    }*/


                    return query ;
                }
                catch (Exception ex)
                {

                   // Log.Logger.Error(ex.Message);
                    return null;

                }

            }
        }

        public void UpdateQuantite(int id_user, int id_cour, string stat)
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                if (stat.Equals("plus"))
                {
                    var qte = db.Paniers.SingleOrDefault(b => b.EtudiantId == id_user &&
                                       b.CoursId == id_cour);
                    qte.Quantite++;
                    db.SaveChanges();
                }
                else if (stat.Equals("moins"))
                {
                    var qte = db.Paniers.SingleOrDefault(b => b.EtudiantId == id_user &&
                   b.CoursId == id_cour);
                    qte.Quantite--;
                    if(qte.Quantite==0)
                        db.Paniers.Remove(qte);
                    db.SaveChanges();
                }
                else
                {
                    var qte = db.Paniers.SingleOrDefault(b => b.EtudiantId == id_user &&
                  b.CoursId == id_cour);
                    db.Paniers.Remove(qte);
                    db.SaveChanges();

                }
            }
        }
         
    }
}