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
// Module app.catalog
//
 
var m = angular.module('app.catalog', [])


// ****************************************************************************
// Configure app.catalog
//

m.config(['$urlRouterProvider', '$stateProvider', 'settings', ($urlRouterProvider: ng.ui.IUrlRouterProvider, $stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) => {
    
    $stateProvider
        .state('app.catalog', {
            url: '/catalog',
            data: {
                roles: ['user']
            },
            controller: 'app.catalog',
            views: {
                'content@': {
                    templateUrl: `${settings.moduleBaseUri}/catalog/catalog.tpl.html`,
                    controller: 'app.catalog'
                }
            }
        })
}])


// ****************************************************************************
// Controller app.catalog
//

interface ICatalogScope {
    clear(): void
    criteria: string
    load(): void
    more(): void
    products: Array<IProduct>
    skip: number
    take: number
}

m.controller('app.catalog', ['$scope', 'repositories', 'services',
    ($scope: ICatalogScope, repositories: IRepositories, services: IServices) => {

        services.logger.debug('Loaded controller app.catalog')
        
        $scope.skip = 0
        $scope.take = 5
        $scope.criteria = null
        
        $scope.clear = () => {
            $scope.criteria = null
            $scope.load()
        }

        $scope.load = () => {
            repositories.products.search($scope.criteria, $scope.skip, $scope.take).then((data) => {
                $scope.products = data
            })
        }

        $scope.more = () => {
            $scope.take = $scope.take + 10
            $scope.load()
        }

        $scope.load()
    }
]) 