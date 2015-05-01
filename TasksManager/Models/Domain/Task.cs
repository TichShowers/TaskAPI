using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TasksManager.Models.Domain
{
    public class Task
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public String Status { get; set; }
        public virtual User UserFor { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DetetedAt { get; set; }

        public Task()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}