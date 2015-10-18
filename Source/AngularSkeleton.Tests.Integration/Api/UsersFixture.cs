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
using System.Net;
using System.Net.Http;
using AngularSkeleton.Service;
using AngularSkeleton.Service.Model.Accounts;
using Moq;
using Shouldly;
using Xbehave;

namespace AngularSkeleton.Tests.Integration.Api
{
    /// <summary>
    ///     Fixture for testing products endpoint
    /// </summary>
    public class UsersFixture : ApiFixtureBase
    {
        protected Mock<IAccountService> AccountManagementServiceMock;

        public UsersFixture()
        {
            AccountManagementServiceMock = new Mock<IAccountService>();
            ServiceFacadeMock.Setup(m => m.AccountManagement).Returns(AccountManagementServiceMock.Object);
        }

        [Scenario]
        public void RetrievingCurrentUser(UserModel user)
        {
            "Given an existing user".
                f(() => AccountManagementServiceMock.Setup(m => m.GetCurrentUserAsync()).ReturnsAsync(new UserModel {Id = 1}));

            "When it is retrieved".
                f(() =>
                {
                    Request.RequestUri = new Uri(string.Format("{0}/me", UsersUrl));
                    Response = Client.SendAsync(Request).Result;
                    user = Response.Content.ReadAsAsync<UserModel>().Result;
                });

            "And a '200 OK' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "Then the user should be retrieved".
                f(() => AccountManagementServiceMock.Verify(m => m.GetCurrentUserAsync(), Times.Once()));

            "Then it is returned".
                f(() => user.ShouldNotBe(null));

            "And it should have an id".
                f(() => user.Id.ShouldBe(1));
        }
    }
}