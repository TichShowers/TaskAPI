using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using TasksManager.Models.DAL.Contracts;
using TasksManager.Models.Domain;

namespace TasksManager.Models.DAL.Repositories
{
    public class UserRepository : EfRepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
            
        }
    }
}