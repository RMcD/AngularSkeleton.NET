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
// Interface IProduct
//

interface IProduct extends restangular.IElement {
    active: boolean
    description: string
    id: number
    name: string
    quantityAvailable: number
    dateAdded: Date
}



// ****************************************************************************
// Interface IProductsRepository
//

interface IProductsRepository {
    create(product: IProduct): angular.IPromise<IProduct>
    find(id: string): angular.IPromise<IProduct>
    getAll(): angular.IPromise<Array<IProduct>>
    remove(product: IProduct): angular.IPromise<any>
    save(product: IProduct): angular.IPromise<any>
    search(criteria: string, skip: number, take: number): angular.IPromise<IPagedResult<IProduct>>
    toggle(product: IProduct): angular.IPromise<any>
}


// ****************************************************************************
// Module app.repositories.products
//

var m = angular.module('app.repositories.products', ['restangular'])


// ****************************************************************************
// Products repository
//

m.factory('products', ['Restangular', (restangular: restangular.IService) => {

    function create(client: IProduct) {
        return <angular.IPromise<IProduct>>restangular.all('products').post(client)
    }

    function find(id: string) {
        return <angular.IPromise<IProduct>>restangular.one('products', id).get()
    }

    function getAll() {
        return <angular.IPromise<Array<IProduct>>>restangular.all('products').getList()
    }

    function remove(product: IProduct) {
        return product.remove()
    }

    function save(product: IProduct) {
        return product.put()
    }

    function search(criteria: string, skip: number, take: number) {
        return <angular.IPromise<IPagedResult<IProduct>>>restangular.all('products').customGET('', {criteria: criteria, skip: skip, take: take})
    }

    function toggle(product: IProduct) {
        return product.customPOST(null, 'toggle')
    }

    var repository: IProductsRepository = {
        create: create,
        find: find,
        getAll: getAll,
        remove: remove,
        save: save,
        search: search,
        toggle: toggle
    }

    return repository
}]) 