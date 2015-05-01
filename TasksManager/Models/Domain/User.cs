using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TasksManager.Models.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual IList<Task> Tasks { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public User()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}