using System.Web.Http;
using AngularSkeleton.NET.WebApplication.Infrastructure.Attributes;
using CuttingEdge.Conditions;

namespace AngularSkeleton.WebApplication.Controllers
{
    /// <summary>
    ///     Base controller.
    /// </summary>
    [Authorize]
    [ExceptionHandling]
    public abstract class ControllerBase : ApiController
    {
        /// <summary>
        ///     Protected abstract constructor.
        /// </summary>
        /// <param name="services">Instance of the services facade</param>
        protected ControllerBase(IServiceFacade services)
        {
            Condition.Requires(services, "services").IsNotNull();
            Services = services;
        }

        /// <summary>
        ///     The tenant secure context.
        /// </summary>
        protected IServiceFacade Services { get; private set; }
    }
}