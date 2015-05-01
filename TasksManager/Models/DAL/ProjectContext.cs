using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TasksManager.Models.DAL.Mappers;
using TasksManager.Models.Domain;

namespace TasksManager.Models.DAL
{
    public class ProjectContext : DbContext
    {

        public ProjectContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new ProjectInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UserMapper());
            modelBuilder.Configurations.Add(new TaskMapper());
        }
    }
}