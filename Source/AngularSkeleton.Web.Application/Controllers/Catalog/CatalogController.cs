using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AngularSkeleton.Common;
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Service;
using AngularSkeleton.Service.Model.Products;

namespace AngularSkeleton.Web.Application.Controllers.Catalog
{
    /// <summary>
    ///     Controller for accessing the catalog
    /// </summary>
    [RoutePrefix(Constants.Api.Version.RestV1CatalogRoutePrefix)]
    public class CatalogController : ControllerBase
    {
        /// <summary>
        ///     Creates a new catalog controller instance
        /// </summary>
        /// <param name="services">The service facade</param>
        public CatalogController(IServiceFacade services) : base(services)
        {
        }

        /// <summary>
        ///     Searches products
        /// </summary>
        /// <remarks>Returns a collection of products</remarks>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A client was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("search")]
        [AcceptVerbs("GET")]
        [ResponseType(typeof (ICollection<ProductModel>))]
        public async Task<HttpResponseMessage> SearchAsync(string criteria = null, int skip = 0, int take = 10)
        {
            var options = new QueryOptions {Skip = skip, Take = take};
            var products = await Services.Catalog.SearchAsync(options, criteria);
            return Request.CreateResponse(products);
        }
    }
}