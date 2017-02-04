namespace Practice.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Practice.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Practice.Models.DatabaseContext context)
        {
            //context.Database.ExecuteSqlCommand("TRUNCATE TABLE Responses");
            //context.Database.ExecuteSqlCommand("TRUNCATE TABLE Users");
            //context.Database.ExecuteSqlCommand("TRUNCATE TABLE Projects");
            //context.SaveChanges();

            //var users = new List<Users>
            //{
            //    new Users { FirstName = "Stan", LastName = "Fisherman", Email = "stan@gmail.com", Password = "password", PhoneNumber = "021 531 243" },
            //    new Users { FirstName  = "Hugh", LastName = "Mungus", Email = "hugh@gmail.com", Password = "password", PhoneNumber = "021 577 633" },
            //    new Users { FirstName  = "Steve", LastName = "Jobs", Email = "steve@gmail.com", Password = "password", PhoneNumber = "021 346 263" }
            //};

            //users.ForEach(s => context.Users.AddOrUpdate(s));

            //var projects = new List<Projects>
            //{
            //    new Projects{ Id = 123, Name = "Kiwi", Description = "Lol", Time = 2, Incentive = 200, Notes = " " },
            //    new Projects { Id = 321, Name = "Plane", Description = "Lol", Time = 2, Incentive = 200, Notes = " " },
            //};

            //projects.ForEach(s => context.Projects.AddOrUpdate(s));

            //var responses = new List<Responses>
            //{
            //    new Responses {UserId = 1, ProjectId = 123, FirstName = "Stan", LastName= "Fisherman", Email = "stan@gmail.com", PhoneNumber = "021 231 50393", Checked = false },
            //    new Responses {UserId = 2, ProjectId = 321, FirstName = "Hugh", LastName= "D", Email = "s@gmail.com", PhoneNumber = "021 631 50393", Checked = false }
            //};

            //responses.ForEach(s => context.Responses.AddOrUpdate(s));
            //context.SaveChanges();
        }
    }
}
