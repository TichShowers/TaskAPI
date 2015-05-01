using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TasksManager.Models.Domain;
using TasksManager.Models.DTO;

namespace TasksManager.Infrastructure
{
    public static class UserExtensions
    {
        public static UserDto ConvertUserToDto(this User user)
        {
            return new UserDto(){ Id = user.Id, Name= user.Name, CreatedAt = user.CreatedAt };
        } 
    }

    public static class TaskExtensions
    {
        public static TaskDto ConvertTaskToDto(this Task task)
        {
            return new TaskDto()
            {
                Id = task.Id,
                Description = task.Description,
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                UserFor = task.UserFor.Id
            };
        }
    }
}