using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Plateforme_Thot_Entity.Models
{
    public class Plateforme_Thot_Data_2:DbContext
    {
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Cours> Cours { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<Inscription_Cours> Inscription_Cours { get; set; }
        public DbSet<Materiel_Didactiques> Materiel_Didactiques { get; set; }
        public DbSet<Panier> Paniers { get; set; }


        public Plateforme_Thot_Data_2()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Plateforme_Thot_Entity.Models.Plateforme_Thot_Data_2, Plateforme_Thot_Entity.Migrations.Configuration>());
        }

       // public System.Data.Entity.DbSet<Plateforme_Thot_Entity.Models.Affiche> Affiches { get; set; }
    }
}