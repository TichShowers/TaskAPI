using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using TasksManager.Models.DAL.Contracts;
using TasksManager.Models.Domain;

namespace TasksManager.Models.DAL.Repositories
{
    public class TaskRepository : EfRepository<Task>
    {
        public TaskRepository(DbContext dbContext) : base(dbContext)
        {
            
        }

        public IEnumerable<Task> GetAllExistingTasks()
        {
            return DbSet.ToList().Where(t => t.DetetedAt == null);
        }
    }
}