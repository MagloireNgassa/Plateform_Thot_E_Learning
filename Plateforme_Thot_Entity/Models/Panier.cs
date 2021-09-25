using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using Plateforme_Thot_Entity.Models;
using Serilog;

namespace Plateforme_Thot_Entity.Models
{
    public class Panier
    {
        public int PanierId { get; set; }
        public int EtudiantId { get; set; }
        public int CoursId { get; set; }
        [DefaultValue(1)]
        public int Quantite { get; set; }
        [DefaultValue(false)]
        public bool Statut { get; set; }
 

        public virtual Cours Cours { get; set; }
        public virtual Etudiant Etudiant { get; set; }

       public Panier(int EtudiantId, int CoursId )
        {
            this.EtudiantId = EtudiantId;
            this.CoursId = CoursId;
            this.Quantite = 1;
            this.Statut = false;
           
        }

        public void CreatePanier()
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                //Panier pan = new Panier(this.EtudiantId = EtudiantId, this.CoursId = CoursId);
                 db.Paniers.Add(this);
                db.SaveChanges();

            }   
        }


        public decimal SommeProduit()
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                var Prix = db.Cours.Find(this.CoursId);

                return (Prix.Prix * this.Quantite);
            }     
        }

        public void AddProduit()
        {
            using (Plateforme_Thot_Data_2 db = new Plateforme_Thot_Data_2())
            {
                /* var refId = db.Paniers.Where(
                     c => c.EtudiantId == etudiantId &&
                     c.CoursId == coursId).FirstOrDefault();*/
                try
                {
                    var refId = (from b in db.Paniers
                                 where b.EtudiantId == this.EtudiantId &&
                                       b.CoursId == this.CoursId
                                 select b).Count();
                    if (refId == 0)
                    {
                        CreatePanier();
                    }
                    else
                    {
 
                    }
                }
                catch(Exception ex) 
                { 
                    
                }

                
            }

        }

        /*public void DelPanier()
        {
            using (Plateforme_Thot_Data db = new Plateforme_Thot_Data())
            { 
                
            }
        }*/
    }
}