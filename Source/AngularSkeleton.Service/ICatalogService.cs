//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Threading.Tasks;
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Service.Model.Products;

namespace AngularSkeleton.Service
{
    /// <summary>
    ///     Service for accessing the product catalog
    /// </summary>
    public interface ICatalogService
    {
        /// <summary>
        ///     Creates a product
        /// </summary>
        /// <param name="model">The client model</param>
        Task<ProductModel> CreateProductAsync(ProductAddModel model);

        /// <summary>
        ///     Deletes a product
        /// </summary>
        /// <param name="productId">The product id</param>
        Task<int> DeleteProductAsync(long productId);

        /// <summary>
        ///     Retrieves a single product
        /// </summary>
        /// <param name="productId">The product id</param>
        Task<ProductModel> GetProductAsync(long productId);

        /// <summary>
        ///     Searches for products
        /// </summary>
        /// <param name="options">The query paging options</param>
        /// <param name="criteria">The optional search criteria</param>
        /// <returns>A paged result of products</returns>
        Task<PagedResult<ProductModel>> SearchProductsAsync(QueryOptions options, string criteria = null);
    }
}