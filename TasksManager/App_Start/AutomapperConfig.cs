using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TasksManager.Models.Domain;
using TasksManager.Models.DTO;

namespace TasksManager
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<UserDto, User>();

            Mapper.CreateMap<Task, TaskDto>();
            Mapper.CreateMap<TaskDto, Task>();
        }
    }
}