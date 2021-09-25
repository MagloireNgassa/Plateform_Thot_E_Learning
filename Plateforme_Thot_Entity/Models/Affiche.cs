using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plateforme_Thot_Entity.Models
{
    public class Affiche
    {
        public int AfficheId { get; set; }
        public string Images { get; set; }
        public string Nom { get; set; }
        public int Quantite { get; set; }
        public int Id_Produit { get; set; }

        public Affiche()
        {
            this.Images = Images;
            this.Nom = Nom;
            this.Quantite = Quantite;
            this.Id_Produit = Id_Produit;
        }
        public Affiche(string Images, string Nom, int Quantite,int Id_Produit)
        {
            this.Images = Images;
            this.Nom = Nom;
            this.Quantite = Quantite;
            this.Id_Produit = Id_Produit;
        }
    }
}