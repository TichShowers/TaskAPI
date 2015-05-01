using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TasksManager.Models.Domain;

namespace TasksManager.Models.DAL.Mappers
{
    public class TaskMapper : EntityTypeConfiguration<Task>
    {
        public TaskMapper()
        {
            HasKey(t => t.Id);

            HasRequired(t => t.UserFor).WithMany(u => u.Tasks);
        }
    }
}