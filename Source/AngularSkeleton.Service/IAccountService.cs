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
using System.Threading.Tasks;
using AngularSkeleton.Domain.Accounts;
using AngularSkeleton.Service.Model.Users;

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
        ///     Creates a new user.
        /// </summary>
        Task<UserModel> CreateUserAsync(UserAddModel model);

        /// <summary>
        ///     Removes a user.
        /// </summary>
        /// <param name="userId">The user id</param>
        Task<int> DeleteUserAsync(long userId);

        /// <summary>
        ///     Returns a collection of all users.
        /// </summary>
        Task<IEnumerable<UserModel>> GetAllUsersAsync();

        /// <summary>
        ///     Returns the currently logged-in user
        /// </summary>
        Task<UserModel> GetCurrentUserAsync();

        /// <summary>
        ///     Returns a user with given id.
        /// </summary>
        /// <param name="userId">The user id</param>
        Task<UserModel> GetUserAsync(long userId);

        /// <summary>
        ///     Resets a user's password and sends an email with change token.
        /// </summary>
        /// <param name="userId">The userId</param>
        Task<bool> ResetPasswordAsync(long userId);

        /// <summary>
        ///     Toggles a user's active status.
        /// </summary>
        /// <param name="userId">The user id</param>
        Task<int> ToggleUserAsync(long userId);

        /// <summary>
        ///     Updates a user.
        /// </summary>
        /// <param name="model">The user model</param>
        /// <param name="userId">The user id</param>
        Task<int> UpdateUserAsync(UserUpdateModel model, long userId);

        /// <summary>
        ///     Indicates if a username is already in-use
        /// </summary>
        /// <param name="username">The user name</param>
        Task<bool> UsernameExistsAsync(string username);
    }
}