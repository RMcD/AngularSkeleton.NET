// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;
using AngularSkeleton.Common;
using AngularSkeleton.Common.Exceptions;
using AngularSkeleton.DataAccess.Repositories;
using AngularSkeleton.Domain.Accounts;
using AngularSkeleton.Domain.Security;
using AngularSkeleton.Service.Model.Accounts;
using AutoMapper;
using CuttingEdge.Conditions;

namespace AngularSkeleton.Service.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IRepositoryFacade _repositories;

        public AccountService(IRepositoryFacade repositories)
        {
            Condition.Requires(repositories, "repositories").IsNotNull();
            _repositories = repositories;
        }

        public async Task<User> AuthorizeAsync(string username, string password)
        {
            var user = await _repositories.Users.FindByUsernameAsync(username);

            if (null == user)
                return null;

            return (user.VerifyPassword(password)) ? user : null;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.Permissions.User)]
        public async Task<UserModel> GetCurrentUserAsync()
        {
            var principal = Principal.Current;
            if (null == principal)
                throw new SecurityException("Unauthorized");

            var user = await _repositories.Users.FindAsync(principal.UserId);
            if (user == null)
                throw new NotFoundException("The user was not found");

            return Mapper.Map<User, UserModel>(user);
        }
    }
}