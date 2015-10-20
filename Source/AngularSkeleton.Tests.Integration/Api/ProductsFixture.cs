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
using System.Net.Http.Formatting;
using AngularSkeleton.DataAccess.Util;
using AngularSkeleton.Service;
using AngularSkeleton.Service.Model.Products;
using Moq;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xbehave;

namespace AngularSkeleton.Tests.Integration.Api
{
    /// <summary>
    ///     Fixture for testing products endpoint
    /// </summary>
    public class ProductsFixture : ApiFixtureBase
    {
        protected Mock<ICatalogService> CatalogManagementServiceMock;

        public ProductsFixture()
        {
            CatalogManagementServiceMock = new Mock<ICatalogService>();
            ServiceFacadeMock.Setup(m => m.CatalogManagement).Returns(CatalogManagementServiceMock.Object);
        }

        /// <summary>
        ///     PUT /products
        /// </summary>
        [Scenario]
        public void CreatingAProduct(dynamic newProduct)
        {
            "Given a new product".
                f(() =>
                {
                    newProduct = JObject.FromObject(new
                    {
                        name = "TEST_PRODUCT",
                        description = "TEST_DESCRIPTION",
                        quantityAvailable = 250
                    });
                });

            "When a POST request is made".
                f(async () =>
                {
                    CatalogManagementServiceMock.Setup(m => m.CreateProductAsync(It.IsAny<ProductAddModel>())).ReturnsAsync(new ProductModel {Id = 1});
                    Request.Method = HttpMethod.Post;
                    Request.RequestUri = new Uri(ProductsUrl);
                    Request.Content = new ObjectContent<dynamic>(newProduct, new JsonMediaTypeFormatter());
                    Response = await Client.SendAsync(Request);
                });

            "Then a '201 Created' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.Created));

            "Then the product should be saved".
                f(() => CatalogManagementServiceMock.Verify(m => m.CreateProductAsync(It.IsAny<ProductAddModel>()), Times.Once()));

            "And the reponse header location should be set".
                f(() => Response.Headers.Location.ShouldNotBe(null));
        }

        /// <summary>
        ///     DELETE /products/{id}
        /// </summary>
        [Scenario]
        [Example(1)]
        public void DeletingAProduct(long productId)
        {
            "Given an existing product".
                f(() => CatalogManagementServiceMock.Setup(m => m.DeleteProductAsync(productId)).ReturnsAsync(1));

            "When a DELETE request is made".
                f(() =>
                {
                    Request.Method = HttpMethod.Delete;
                    Request.RequestUri = new Uri(string.Format("{0}/{1}", ProductsUrl, productId));
                    Request.Content = new ObjectContent<dynamic>(new JObject(), new JsonMediaTypeFormatter());
                    Response = Client.SendAsync(Request).Result;
                });

            "Then a 200 OK status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "Then the contact should be removed".
                f(() => CatalogManagementServiceMock.Verify(m => m.DeleteProductAsync(productId), Times.Once()));
        }

        /// <summary>
        ///     GET /products?criteria={0}
        /// </summary>
        [Scenario]
        public void RetrievingAllProducts(ICollection<ProductModel> products, IList<ProductModel> productsResult)
        {
            "Given existing products".
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
                    CatalogManagementServiceMock.Setup(m => m.SearchProductsAsync(It.IsAny<QueryOptions>(), "test")).ReturnsAsync(new PagedResult<ProductModel>(products));
                    Request.RequestUri = new Uri(string.Format("{0}?criteria={1}", ProductsUrl, "test"));
                    Response = await Client.SendAsync(Request);
                    productsResult = await Response.Content.ReadAsAsync<IList<ProductModel>>();
                });

            "Then a '200 OK' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "Then they are returned".
                f(() => productsResult.Count().ShouldBe(products.Count()));
        }

        /// <summary>
        ///     GET /products/{id}
        /// </summary>
        [Scenario]
        public void RetrievingAProduct(ProductModel product, long productId)
        {
            "Given an existing product".
                f(() => CatalogManagementServiceMock.Setup(m => m.GetProductAsync(productId)).ReturnsAsync(new ProductModel {Id = productId}));

            "When it is retrieved".
                f(() =>
                {
                    Request.RequestUri = new Uri(string.Format("{0}/{1}", ProductsUrl, productId));
                    Response = Client.SendAsync(Request).Result;
                    product = Response.Content.ReadAsAsync<ProductModel>().Result;
                });

            "Then the product should be retrieved".
                f(() => CatalogManagementServiceMock.Verify(m => m.GetProductAsync(productId), Times.Once()));

            "And a '200 OK' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "Then it is returned".
                f(() => product.ShouldNotBe(null));

            "And it should have an id".
                f(() => product.Id.ShouldBe(productId));
        }
    }
}