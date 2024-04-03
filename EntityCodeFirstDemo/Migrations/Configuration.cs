namespace EntityCodeFirstDemo.Migrations
{
    using EntityCodeFirstDemo.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityCodeFirstDemo.Models.StudentDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EntityCodeFirstDemo.Models.StudentDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            Trainer t1 = new Trainer() { Name = "Abhimayu", City = "pune", Experience = 2 };
            context.Trainers.Add(t1);

            Student s1 = new Student() { Name = "Omkar", City = "Baramati", Email = "a@a.com" };
            context.Students.Add(s1);

            context.SaveChanges();
        }
    }
}
