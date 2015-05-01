using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TasksManager.Models.Domain;

namespace TasksManager.Models.DAL.Mappers
{
    public class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            HasKey(u => u.Id);

            HasMany(u => u.Tasks).WithRequired(t => t.UserFor);
        }
    }
}