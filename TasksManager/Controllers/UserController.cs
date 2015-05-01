using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using TasksManager.Infrastructure;
using TasksManager.Models.DAL.Repositories;
using TasksManager.Models.Domain;
using TasksManager.Models.DTO;

namespace TasksManager.Controllers
{
    public class UserController : ApiController
    {
        public UserRepository Repository { get; set; }

        public UserController(UserRepository repository)
        {
            Repository = repository;
        }

        // GET: api/User
        public IEnumerable<UserDto> Get()
        {
            var users = Repository.GetAll().ToList();
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        // GET: api/User/5
        public UserDto Get(int id)
        {
            var user = Repository.GetById(id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Mapper.Map<User, UserDto>(user);
        }

        [Route("api/user/default")]
        public UserDto GetLoggedInUser()
        {
            var user = Repository.GetAll().FirstOrDefault();
            return Mapper.Map<User, UserDto>(user);
        }

        [Route("api/user/location/{id}")]
        public HttpResponseMessage PutNewLocation(int id, LocationDto location)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            var user = Repository.GetById(id);
            if (user == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            try
            {
                user.Latitude = location.Latitude;
                user.Longitude = location.Longitude;
                Repository.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.OK);

                response.Headers.Location = new Uri(Url.Link("DefaultApi", new {Id = user.Id}));
                return response;
            }
            catch (Exception ex)
            {
                var err = new HttpError(ex, true); 
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }

        // POST: api/User
        public HttpResponseMessage Post(UserDto userDto)
        {
            if (!ModelState.IsValid) 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            try
            {
                User user = Mapper.Map<UserDto, User>(userDto);

                Repository.Add(user);
                Repository.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<User, UserDto>(user));

                response.Headers.Location = new Uri(Url.Link("DefaultApi", new {Id = user.Id}));
                return response;
            }
            catch (Exception ex)
            {
                var err = new HttpError(ex, true); 
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        }

        // PUT: api/User/5
        public HttpResponseMessage Put(int id, UserDto userDto)
        {
            if (!ModelState.IsValid) 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            User user = Repository.GetById(id);
            if (user == null) 
                return Request.CreateResponse(HttpStatusCode.NotFound);
            try
            {
                user.Name = userDto.Name;
                Repository.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.OK);

                response.Headers.Location = new Uri(Url.Link("DefaultApi", new {Id = user.Id}));
                return response;
            }
            catch (Exception ex)
            {
                var err = new HttpError(ex, true); 
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
        
        }

        // DELETE: api/User/5
        public HttpResponseMessage Delete(int id)
        {
            User user = Repository.GetById(id);
            if (user != null)
            {
                Repository.Delete(user);
                Repository.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
