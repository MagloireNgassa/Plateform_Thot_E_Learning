namespace Plateforme_Thot_Entity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Plateforme_Thot_Entity.Models.Plateforme_Thot_Data_2>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Plateforme_Thot_Entity.Models.Plateforme_Thot_Data_2";
        }

        protected override void Seed(Plateforme_Thot_Entity.Models.Plateforme_Thot_Data_2 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
