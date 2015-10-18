//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading.Tasks;
using AngularSkeleton.Common;
using AngularSkeleton.Common.Exceptions;
using AngularSkeleton.DataAccess.Repositories;
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Domain.Catalog;
using AngularSkeleton.Service.Model.Catalog;
using AutoMapper;
using CuttingEdge.Conditions;

namespace AngularSkeleton.Service.Impl
{
    public class CatalogService : ICatalogService
    {
        private readonly IRepositoryFacade _repositories;

        public CatalogService(IRepositoryFacade repositories)
        {
            Condition.Requires(repositories, "repositories").IsNotNull();
            _repositories = repositories;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<ProductModel> CreateProductAsync(ProductAddModel model)
        {
            if (await _repositories.Products.AnyAsync(o => o.Name == model.Name))
                throw new BusinessException(string.Format("A product already exists with name: {0}", model.Name));

            var product = new Product(model.Name)
            {
                Description = model.Description,
                QuantityAvailable = model.QuantityAvailable
            };

            _repositories.Products.Insert(product);

            await _repositories.SaveChangesAsync();

            return Mapper.Map<ProductModel>(product);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.Administrator)]
        public async Task<int> DeleteProductAsync(long productId)
        {
            var product = await _repositories.Products.FindAsync(productId);
            if (null == product)
                return 0;

            _repositories.Products.Remove(product);

            return await _repositories.SaveChangesAsync();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.User)]
        public async Task<ProductModel> GetProductAsync(long productId)
        {
            var product = await _repositories.Products.FindAsync(productId);
            if (null == product)
                throw new NotFoundException("No product exists with given id.");

            return Mapper.Map<Product, ProductModel>(product);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.User)]
        public async Task<PagedResult<ProductModel>> SearchProductsAsync(QueryOptions options, string criteria = null)
        {
            var found = await _repositories.Products.Search(options, criteria);
            var models = Mapper.Map<ICollection<ProductModel>>(found.Items);
            return new PagedResult<ProductModel>(models);
        }
    }
}