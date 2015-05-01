using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TasksManager.Models.Domain;

namespace TasksManager.Models.DAL
{
    public class ProjectInitializer : DropCreateDatabaseIfModelChanges<ProjectContext>
    {
        protected override void Seed(ProjectContext context)
        {
            var users = new List<User>()
            {
                new User { Name = "Tich" }, 
                new User { Name = "Rayne" }, 
                new User { Name = "Jonke" }, 
                new User { Name = "Usually Dead" }, 
                new User { Name = "Adria" }
            };

            context.Users.AddRange(users);

            context.SaveChanges();

            var tasks = new List<Task>()
            {
                new Task()
                {
                    Description = "Play Games",
                    Status = "U",
                    UserFor = context.Users.FirstOrDefault(u => u.Name=="Tich")
                },
                new Task()
                {
                    Description = "Move",
                    Status = "F",
                    UserFor = context.Users.FirstOrDefault(u => u.Name=="Usually Dead")
                },
                new Task()
                {
                    Description = "Play Pokémon",
                    Status = "U",
                    UserFor = context.Users.FirstOrDefault(u => u.Name=="Jonke")
                },
                new Task()
                {
                    Description = "Play Hearthstone",
                    Status = "F",
                    UserFor = context.Users.FirstOrDefault(u => u.Name=="Tich")
                },
                new Task()
                {
                    Description = "Be a Vaporeon",
                    Status = "F",
                    UserFor = context.Users.FirstOrDefault(u => u.Name=="Tich")
                },
                new Task()
                {
                    Description = "Make a Cake",
                    Status = "U",
                    UserFor = context.Users.FirstOrDefault(u => u.Name=="Adria")
                },
                new Task()
                {
                    Description = "Buy a Cake",
                    Status = "F",
                    UserFor = context.Users.FirstOrDefault(u => u.Name=="Adria")
                },
                new Task()
                {
                    Description = "Eat a Cake",
                    Status = "F",
                    UserFor = context.Users.FirstOrDefault(u => u.Name=="Adria")
                },
                new Task()
                {
                    Description = "Be a Tide Warden",
                    Status = "U",
                    UserFor = context.Users.FirstOrDefault(u => u.Name=="Tich")
                },
                new Task()
                {
                    Description = "Do",
                    Status = "F",
                    UserFor = context.Users.FirstOrDefault(u => u.Name=="Rayne")
                },
                new Task()
                {
                    Description = "Everythings",
                    Status = "U",
                    UserFor = context.Users.FirstOrDefault(u => u.Name=="Rayne")
                },
                new Task()
                {
                    Description = "Organize Jonkecon",
                    Status = "U",
                    UserFor = context.Users.FirstOrDefault(u => u.Name=="Jonke")
                },
                new Task()
                {
                    Description = "Go to Jonkecon",
                    Status = "U",
                    UserFor = context.Users.FirstOrDefault(u => u.Name=="Tich")
                }
            };

            context.Tasks.AddRange(tasks);

            context.SaveChanges();
        }
    }
}