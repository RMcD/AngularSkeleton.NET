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
using AngularSkeleton.DataAccess.Repositories;
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Service.Model.Products;
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

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.User)]
        public async Task<PagedResult<ProductModel>> SearchAsync(QueryOptions options, string criteria = null)
        {
            var found = await _repositories.Products.Search(options, criteria);
            var models = Mapper.Map<ICollection<ProductModel>>(found.Items);
            return new PagedResult<ProductModel>(models, found.TotalRecords);
        }
    }
}