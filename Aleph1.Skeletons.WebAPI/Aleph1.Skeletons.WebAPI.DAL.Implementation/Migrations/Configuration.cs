namespace Aleph1.Skeletons.WebAPI.DAL.Implementation.Migrations
{
    using Aleph1.Skeletons.WebAPI.Models;

    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<PersonContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PersonContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Persons.AddOrUpdate(new Person() { ID = 1, FirstName = "Avraham", LastName = "Essoudry" });
            context.Persons.AddOrUpdate(new Person() { ID = 2, FirstName = "MyFirst", LastName = "MyLast" });

            context.SaveChanges("Seed");
        }
    }
}
