//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================


// ****************************************************************************
// Interfaces IRepositories
//

interface IRepositories {
    authentication: IAuthenticationRepository
    clients: IClientsRepository
    users: IUsersRepository
}


// ****************************************************************************
// Module app.repositories
//

var m = angular.module('app.repositories', [
    'app.repositories.authentication',
    'app.repositories.clients',
    'app.repositories.users'
])

// ****************************************************************************
// Service 'repositories'
//

m.factory('repositories', [
    'authentication',
    'clients',
    'users',
    (
        authentication: IAuthenticationRepository,
        clients: IClientsRepository,
        users: IUsersRepository
        ) => {

        var repositories: IRepositories = {
            authentication: authentication,
            clients: clients,
            users: users
        }
        return repositories
    }
])