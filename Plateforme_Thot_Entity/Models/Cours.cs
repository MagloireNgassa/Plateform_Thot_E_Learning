using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Plateforme_Thot_Entity.Models
{
    public class Cours : Produit
    {
        public int CoursId { get; set; }
        [Required]
        [StringLength(30)]
        public string Niveau_Scolaire { get; set; }
        [Required]
        [StringLength(30)]
        public string Nom_Cours { get; set; }
 
        public virtual List<Panier> Paniers { get; set; }
        public virtual Materiel_Didactiques Materiel_Didactiques { get; set; }


    }
}