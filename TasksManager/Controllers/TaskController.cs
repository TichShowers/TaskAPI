using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using TasksManager.Infrastructure;
using TasksManager.Models.DAL.Repositories;
using TasksManager.Models.Domain;
using TasksManager.Models.DTO;

namespace TasksManager.Controllers
{
    public class TaskController : ApiController
    {
        public TaskRepository TaskRepository { get; set; }
        public UserRepository UserRepository { get; set; }

        public TaskController(TaskRepository taskRepository, UserRepository userRepository)
        {
            TaskRepository = taskRepository;
            UserRepository = userRepository;
        }

        // GET: api/Task
        public IEnumerable<TaskDto> Get()
        {
            var tasks = TaskRepository.GetAllExistingTasks();
            return tasks.Select(t => t.ConvertTaskToDto());
        }

        // GET: api/Task/5
        public TaskDto Get(int id)
        {
            var task = TaskRepository.GetById(id);
            if (task == null || task.DetetedAt != null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);                
            }
            return task.ConvertTaskToDto();
        }

        // POST: api/Task
        public HttpResponseMessage Post(TaskDto value)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            var user = UserRepository.GetById(value.UserFor);
            if (user == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            try
            {
                var task = new Task() {Description = value.Description, Status = value.Status, UserFor = user};

                TaskRepository.Add(task);
                TaskRepository.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.Created, task.ConvertTaskToDto());

                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { Id = task.Id }));
                return response;
            }
            catch (Exception ex)
            {
                var err = new HttpError(ex, true);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }

        // PUT: api/Task/5
        public HttpResponseMessage Put(int id, TaskDto taskDto)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            var task = TaskRepository.GetById(id);
            var user = UserRepository.GetById(taskDto.UserFor);

            if (task == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            if (user == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                task.Description = taskDto.Description;
                task.Status = taskDto.Status;
                task.UserFor = user;
                TaskRepository.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.OK, task.ConvertTaskToDto());

                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { Id = user.Id }));
                return response;
            }
            catch (Exception ex)
            {
                var err = new HttpError(ex, true);
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }

        // DELETE: api/Task/5
        public HttpResponseMessage Delete(int id)
        {
            Task task = TaskRepository.GetById(id);
            if (task != null)
            {
                task.DetetedAt = DateTime.UtcNow;
                TaskRepository.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
