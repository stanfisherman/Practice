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
            //context.Database.ExecuteSqlCommand("TRUNCATE TABLE Response");
            //context.Database.ExecuteSqlCommand("TRUNCATE TABLE User");
            //context.Database.ExecuteSqlCommand("TRUNCATE TABLE Project");
            //context.SaveChanges();

            //var users = new List<User>
            //{
            //    new User { FirstName = "Stan", LastName = "Fisherman", Email = "stan@gmail.com", Password = "password", PhoneNumber = "021 531 243" },
            //    new User { FirstName  = "Hugh", LastName = "Mungus", Email = "hugh@gmail.com", Password = "password", PhoneNumber = "021 577 633" },
            //    new User { FirstName  = "Steve", LastName = "Jobs", Email = "steve@gmail.com", Password = "password", PhoneNumber = "021 346 263" }
            //};

            //users.ForEach(s => context.User.AddOrUpdate(s));

            //var projects = new List<Project>
            //{
            //    new Project{ ProjectId = 123, Name = "Kiwi", Description = "Lol", Time = 2, Incentive = 200, Notes = " " },
            //    new Project { ProjectId = 321, Name = "Plane", Description = "Lol", Time = 2, Incentive = 200, Notes = " " },
            //};

            //projects.ForEach(s => context.Project.AddOrUpdate(s));

            //var responses = new List<Response>
            //{
            //    new Response {FirstName = "Stan", LastName= "Fisherman", Email = "stan@gmail.com", PhoneNumber = "021 231 50393", Checked = false, UserId = 1, ProjectId = 321 },
            //    new Response {FirstName = "Hugh", LastName= "D", Email = "s@gmail.com", PhoneNumber = "021 631 50393", Checked = false, UserId = 2, ProjectId = 123 }
            //};

            //responses.ForEach(s => context.Response.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}
