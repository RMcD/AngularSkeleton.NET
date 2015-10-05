using System.Web.Http;
using AngularSkeleton.WebApplication.Controllers;
using Autofac;
using Autofac.Integration.WebApi;
using CuttingEdge.Conditions;

namespace AngularSkeleton.WebApplication.Infrastructure.Config
{
    /// <summary>
    ///     Configures the IOC container (Autofac)
    /// </summary>
    public class ContainerConfig
    {
        /// <summary>
        ///     Registers the configuration
        /// </summary>
        public static void Register(HttpConfiguration configuration, IServiceFacade serviceFacade = null)
        {
            Condition.Requires(configuration, "configuration").IsNotNull();

            var builder = new ContainerBuilder();

            builder.RegisterModule(new ServicesModule(serviceFacade));

            // controllers
            builder.RegisterApiControllers(typeof (ControllerBase).Assembly);

            // request
            builder.RegisterHttpRequestMessage(configuration);

            // set resolver
            var container = builder.Build();

            // Set the dependency resolver for Web API.
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = webApiResolver;
        }

        /// <summary>
        ///     Registers the autofac configuration.
        /// </summary>
        /// <param name="configuration"></param>
        public static void RegisterAutofacConfig(HttpConfiguration configuration)
        {
            Register(configuration);
        }
    }