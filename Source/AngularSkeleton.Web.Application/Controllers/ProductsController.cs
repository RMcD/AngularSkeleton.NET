//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AngularSkeleton.Common;
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Service;
using AngularSkeleton.Service.Model.Products;
using AngularSkeleton.Web.Application.Infrastructure.Attributes;

namespace AngularSkeleton.Web.Application.Controllers
{
    /// <summary>
    ///     Controler for accessing <see cref="ProductModel" /> instances
    /// </summary>
    [RoutePrefix(Constants.Api.Version.RestV1RoutePrefix)]
    public class ProductsController : ControllerBase
    {
        private const string RetrieveProductRoute = "GetProductById";

        /// <summary>
        ///     Creates a new products controller instance
        /// </summary>
        /// <param name="services"></param>
        public ProductsController(IServiceFacade services) : base(services)
        {
        }

        /// <summary>
        ///     Get a single product
        /// </summary>
        /// <remarks>Returns a single client, specified by the id parameter.</remarks>
        /// <param name="id">The id of the desired client</param>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A client was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("products/{id:long}", Name = RetrieveProductRoute)]
        [AcceptVerbs("GET")]
        [ResponseType(typeof (ProductModel))]
        public async Task<HttpResponseMessage> GetSingleAsync(long id)
        {
            var product = await Services.CatalogManagement.GetProductAsync(id);
            return Request.CreateResponse(product);
        }

        /// <summary>
        ///     Searches products
        /// </summary>
        /// <remarks>Returns a collection of products</remarks>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A client was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("products")]
        [AcceptVerbs("GET")]
        [ResponseType(typeof (ICollection<ProductModel>))]
        public async Task<HttpResponseMessage> SearchAsync(string criteria = null, int skip = 0, int take = 10)
        {
            var options = new QueryOptions {Skip = skip, Take = take};
            var products = await Services.CatalogManagement.SearchProductsAsync(options, criteria);

            return Request.CreateResponse(products);
        }

        /// <summary>
        ///     Create a product
        /// </summary>
        /// <remarks>Creates a new product</remarks>
        /// <param name="model">The product data</param>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("products")]
        [AcceptVerbs("POST")]
        [CheckModelForNull]
        [ValidateModel]
        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<HttpResponseMessage> CreateAsync(ProductAddModel model)
        {
            var product = await Services.CatalogManagement.CreateProductAsync(model);

            var response = Request.CreateResponse(HttpStatusCode.Created);
            var uri = Url.Link(RetrieveProductRoute, new {id = product.Id});
            response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        ///     Delete a product
        /// </summary>
        /// <remarks>Deletes a single product, specified by the id parameter.</remarks>
        /// <param name="id">The ID of the desired product</param>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="404">A product was not found with given id</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("products/{id:long}")]
        [AcceptVerbs("DELETE")]
        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<HttpResponseMessage> DeleteAsync(long id)
        {
            await Services.CatalogManagement.DeleteProductAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}