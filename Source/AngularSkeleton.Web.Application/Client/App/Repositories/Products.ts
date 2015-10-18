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
// Interface Client
//

interface IClient extends restangular.IElement {
    address: string
    archived: boolean
    currencyCode: string
    hasActiveProjects: boolean
    id: string
    name: string
}

// ****************************************************************************
// Interface IClientsRepository
//

interface IClientsRepository {
    find(id: string): angular.IPromise<IClient>
    getAll(): angular.IPromise<Array<IClient>>
    create(client: IClient): angular.IPromise<IClient>
    remove(client: IClient): angular.IPromise<any>
    save(client: IClient): angular.IPromise<any>
    toggle(client: IClient): angular.IPromise<any>
}


// ****************************************************************************
// Module app.repositories.clients
//

var m = angular.module('app.repositories.clients', ['restangular'])


// ****************************************************************************
// Clients repository
//

m.factory('clients', ['Restangular', (restangular: restangular.IService) => {

    function create(client: IClient) {
        return <angular.IPromise<IClient>> restangular.all('clients').post(client)
    }

    function find(id: string) {
        return <angular.IPromise<IClient>> restangular.one('clients', id).get()
    }

    function getAll() {
        return <angular.IPromise<Array<IClient>>> restangular.all('clients').getList()
    }

    function remove(client: IClient) {
        return client.remove()
    }

    function save(client: IClient) {
        return client.put()
    }

    function toggle(client: IClient) {
        return client.customPOST(null, 'toggle')
    }

    var repository: IClientsRepository = {
        create: create,
        find: find,
        getAll: getAll,
        remove: remove,
        save: save,
        toggle: toggle
    }

    return repository
}]) 