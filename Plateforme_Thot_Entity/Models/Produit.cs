using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Plateforme_Thot_Entity.Models
{
    public class Produit
    {
        
        [StringLength(30)]
        public string Image { get; set; }
         
        [StringLength(300)]
        public string Description { get; set; }
        [DefaultValue(0.00)]
        public decimal Prix { get; set; }

       
    }
}