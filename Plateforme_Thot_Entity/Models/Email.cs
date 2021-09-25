using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plateforme_Thot_Entity.Models
{
    public class Email
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}