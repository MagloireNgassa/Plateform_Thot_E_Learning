using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Plateforme_Thot_Entity.Models
{
    public class Etudiant
    {
        public int EtudiantId { get; set; }
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
 
        public string Niveau_Scolaire { get; set; }
        [Required]
        [StringLength(30)]
        public string Email { get; set; }
        [Required]
        [StringLength(1)]
        public string Statut { get; set; }
 
        public virtual List<Inscription_Cours> Inscription_Cours { get; set; }
        public virtual List<Panier> Paniers { get; set; }

    
        public void setLogin()
        {
            this.Login = this.Email;
        }

        public string getLogin()
        {
            return Login;
        }

        public void setPassword()
        {
            Password = this.Nom + "123";
        }
        public string getPassword()
        {
            return Password;
        }

        public void setStatut()
        {
            Statut = "0";
        }

        public string getStatut()
        {
            return Statut;
        }


        //construction de l'email
        public Email constEmail()
        {
            Email email = new Email();
            email.To = this.Email;
            email.Subject = "Votre Login et Password";
            email.Message = "Voici votre Login: " + this.Login + " et votre Password: " + this.Password +
               ". vous devez les changer des votre première connexion ";
             
            return email;
        }

        //fonction pour l,envoie de l,email
        public bool sendMail(Email email)
        {
            try
            {
                string MailSender = System.Configuration.ConfigurationManager.AppSettings["MailSender"].ToString();
                string MailPw = System.Configuration.ConfigurationManager.AppSettings["MailPw"].ToString();

                SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 100000;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(MailSender, MailPw);

                MailMessage mailMessage = new MailMessage(MailSender, email.To, email.Subject, email.Message);
                mailMessage.BodyEncoding = System.Text.UTF8Encoding.UTF8;

                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



    }
}