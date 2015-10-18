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
// Interface User
//
 
interface IUser extends restangular.IElement {
    id?: string
    avatar: string
    email: string
    lastLogin: Date
    nameFirst: string
    nameLast: string
    permission: string
    timezoneUtcOffset: number
    fullName(): string
}


// ****************************************************************************
// Interface IUsersRepository
//

interface IUsersRepository {
    me(): angular.IPromise<IUser>
}


// ****************************************************************************
// Module app.repositories.users
//

var m = angular.module('app.repositories.users', ['restangular'])


// ****************************************************************************
// Users repository
//

m.factory('users', ['Restangular', (restangular: restangular.IService) => {

    // extend fullName()

    restangular.extendModel('users', (model : IUser) => {
        model.fullName = function() {
            return this.nameFirst + ' ' + this.nameLast
        }
        return model;
    })
    
    function me() {
        return <angular.IPromise<IUser>> restangular.all('users').customGET('me')
    }


    var repository: IUsersRepository = {
        me: me
    }

    return repository
}]) 