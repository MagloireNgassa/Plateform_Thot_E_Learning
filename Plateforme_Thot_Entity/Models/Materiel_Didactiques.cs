using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Plateforme_Thot_Entity.Models
{
    public class Materiel_Didactiques
    {
        [ForeignKey("Cours")]
        public int Materiel_DidactiquesId { get; set; }
 
        public string CoursId { get; set; }
        [StringLength(100)]
        public string Note { get; set; }
        [StringLength(100)]
        public string Video { get; set; }
        [StringLength(100)]
        public string Labo { get; set; }
        [StringLength(100)]
        public string Exercices { get; set; }
        [StringLength(100)]
        public string Solutionnaires { get; set; }
        [StringLength(100)]
        public string quiz { get; set; }
        public virtual Cours Cours { get; set; }


    }
}