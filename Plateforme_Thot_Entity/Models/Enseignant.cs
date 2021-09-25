using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Plateforme_Thot_Entity.Models
{
    public class Enseignant
    {
        public int EnseignantId { get; set; }
        [Required]
        [StringLength(50)]
        public string Login { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [StringLength(30)]
        public string Nom { get; set; }
        [Required]
        [StringLength(30)]
        public string Prenom { get; set; }
        [Required]
        [StringLength(30)]
        public string Specialisation { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
    }
}