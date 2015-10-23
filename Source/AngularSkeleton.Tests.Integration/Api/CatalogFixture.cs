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
using System.Linq;
using System.Net;
using System.Net.Http;
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Service;
using AngularSkeleton.Service.Model.Products;
using Moq;
using Shouldly;
using Xbehave;

namespace AngularSkeleton.Tests.Integration.Api
{
    /// <summary>
    ///     Fixture for testing catalog endpoint
    /// </summary>
    public class CatalogFixture : ApiFixtureBase
    {
        protected Mock<ICatalogService> CatalogServiceMock;

        public CatalogFixture()
        {
            CatalogServiceMock = new Mock<ICatalogService>();
            ServiceFacadeMock.Setup(m => m.Catalog).Returns(CatalogServiceMock.Object);
        }

        /// <summary>
        ///     GET /catalog/search?criteria={0}
        /// </summary>
        [Scenario]
        public void RetrievingAllProducts(ICollection<ProductModel> products, PagedResult<ProductModel> productsResult)
        {
            "Given existing catalog items".
                f(() =>
                {
                    products = new List<ProductModel>
                    {
                        new ProductModel {Id = 1},
                        new ProductModel {Id = 2},
                        new ProductModel {Id = 3}
                    };
                });

            "When they are retrieved".
                f(async () =>
                {
                    CatalogServiceMock.Setup(m => m.SearchAsync(It.IsAny<QueryOptions>(), "test")).ReturnsAsync(new PagedResult<ProductModel>(products));
                    Request.RequestUri = new Uri(string.Format("{0}?criteria={1}", CatalogSearchUrl, "test"));
                    Response = await Client.SendAsync(Request);
                    productsResult = await Response.Content.ReadAsAsync<PagedResult<ProductModel>>();
                });

            "Then a '200 OK' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "Then they are all returned".
                f(() => productsResult.Items.Count().ShouldBe(products.Count()));
        }
    }
}