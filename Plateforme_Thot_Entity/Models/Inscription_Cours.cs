using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Plateforme_Thot_Entity.Models
{
    public class Inscription_Cours
    {
        public int Inscription_CoursId { get; set; }
      
       
        public int EtudiantId { get; set; }
       
        public int CoursId { get; set; }

        public virtual Etudiant Etudiant { get; set; }
        public virtual Cours Cours { get; set; }


        public void Inscription()
        { 
            
        }

    }
}