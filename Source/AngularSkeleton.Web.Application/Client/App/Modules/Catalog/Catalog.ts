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

interface ICatalogScope  extends angular.IScope {
    clear(): void
    criteria: string
    currentPage: number
    load(): void
    loading: ng.IPromise<any>
    products: Array<IProduct>
    search(): void
    skip: number
    submitted: boolean
    take: number
    totalRecords: number
}

m.controller('app.catalog', ['$scope', 'repositories', 'services',
    ($scope: ICatalogScope, repositories: IRepositories, services: IServices) => {

        services.logger.debug('Loaded controller app.catalog')

        $scope.criteria = null
        $scope.currentPage = 1
        $scope.skip = 0
        $scope.submitted = false
        $scope.take = 5
        $scope.totalRecords = null
        
        $scope.clear = () => {
            $scope.criteria = null
            $scope.submitted = false
            $scope.load()
        }

        $scope.load = () => {
            $scope.loading = repositories.products.search($scope.criteria, $scope.currentPage * $scope.take, $scope.take).then((result) => {
                $scope.products = result.items
                $scope.totalRecords = result.totalRecords
            })
        }
        
        $scope.search = () => {
            $scope.submitted = true
            $scope.load()
        }

        $scope.$watch('currentPage', () => {
            $scope.load()
        });

        $scope.load()
    }
]) 