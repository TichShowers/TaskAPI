using System.Data.Entity;
using Microsoft.Practices.Unity;
using System.Web.Http;
using TasksManager.Models.DAL;
using TasksManager.Models.DAL.Repositories;
using Unity.WebApi;

namespace TasksManager
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<DbContext, ProjectContext>(new PerThreadLifetimeManager());
            container.RegisterType<TaskRepository>();
            container.RegisterType<UserRepository>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}