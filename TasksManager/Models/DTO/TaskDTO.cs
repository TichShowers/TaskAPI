using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TasksManager.Models.DTO
{
    public class TaskDto
    {
        public int Id { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public String Status { get; set; }
        //public UserDTO UserFor { get; set; }
        [Required]
        public int UserFor { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}