using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plateforme_Thot_Entity.Models
{
    public class CollecteItems
    {
        public listeNiveauScolaire Item;
        public enum listeNiveauScolaire
        {
            Universitaire = 1, Secondaire = 2, Primaire = 3
        }
    }
}