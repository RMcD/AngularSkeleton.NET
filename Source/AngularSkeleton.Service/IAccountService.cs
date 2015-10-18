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
using AngularSkeleton.Domain.Accounts;
using AngularSkeleton.Service.Model.Accounts;

namespace AngularSkeleton.Service
{
    /// <summary>
    ///     Service for accessing the product catalog
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        ///     Authorizes a user credentials
        /// </summary>
        /// <remarks>
        ///     Returns null if invalid
        /// </remarks>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>The authenticated user</returns>
        Task<User> AuthorizeAsync(string username, string password);

        /// <summary>
        ///     Returns the currently logged-in user
        /// </summary>
        Task<UserModel> GetCurrentUserAsync();
    }
}