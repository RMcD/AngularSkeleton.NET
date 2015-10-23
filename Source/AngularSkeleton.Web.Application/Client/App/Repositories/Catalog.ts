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
// Interface ICatalogItem
//

interface ICatalogItem extends restangular.IElement {
    archived: boolean
    dateAdded: Date
    description: string
    productId: number
    name: string
    quantityAvailable: number
}

// ****************************************************************************
// Interface ICatalogRepository
//

interface ICatalogRepository {
    search(criteria: string, skip: number, take: number): angular.IPromise<IPagedResult<ICatalogItem>>
}


// ****************************************************************************
// Module app.repositories.catalog
//

var m = angular.module('app.repositories.catalog', ['restangular'])


// ****************************************************************************
// Catalog repository
//

m.factory('catalog', ['Restangular', (restangular: restangular.IService) => {

    function search(criteria: string, skip: number, take: number) {
        return <angular.IPromise<IPagedResult<ICatalogItem>>>restangular.all('catalog').customGET('search', { criteria: criteria, skip: skip, take: take })
    }

    var repository: ICatalogRepository = {
        search: search
    } 

    return repository
}]) 