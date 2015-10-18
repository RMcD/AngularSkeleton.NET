//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AngularSkeleton.Common;
using AngularSkeleton.Service;
using AngularSkeleton.Service.Model.Accounts;

namespace AngularSkeleton.Web.Application.Controllers
{
    /// <summary>
    /// Controller for accessing users
    /// </summary>
    [RoutePrefix(Constants.Api.Version.RestV1RoutePrefix)]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Creates a new users controller
        /// </summary>
        /// <param name="services"></param>
        public UsersController(IServiceFacade services) : base(services)
        {
        }

        /// <summary>
        ///     Me
        /// </summary>
        /// <remarks>Returns the currently logged in user.</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="401">Credentials were not provided</response>
        /// <response code="403">Access was denied to the resource</response>
        /// <response code="500">An unknown error occurred</response>
        [Route("users/me")]
        [AcceptVerbs("GET")]
        [ResponseType(typeof(UserModel))]
        public async Task<HttpResponseMessage> Get()
        {
            var user = await Services.AccountManagement.GetCurrentUserAsync();
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
    }
}