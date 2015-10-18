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
// Module app.catalog.search
//
 
var m = angular.module('app.catalog.search', [])


// ****************************************************************************
// Configure app.catalog.search
//
 
m.config(['$stateProvider', 'settings', ($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) => {

    $stateProvider
        .state('app.catalog.search', {
            url: '/search',
            data: {
                roles: ['user']
            },
            controller: 'app.catalog.search',
            views: {
                'content@': {
                    templateUrl: `${settings.moduleBaseUri}/catalog/search/search.tpl.html`,
                    controller: 'app.home.welcome'
                }
            },
            resolve: {
                me: ['repositories', (repositories: IRepositories) => repositories.users.me()]
            }
        })
}])


// ****************************************************************************
// Controller app.catalog.search
//

interface ICatalogSearchScope {
    me: IUser
}

m.controller('app.home.welcome', ['$scope', 'me', 'repositories', 'services',
    ($scope: ICatalogSearchScope, me: IUser, repositories: IRepositories, services: IServices) => {

        services.logger.debug('Loaded controller app.catalog.search')

        //repositories.account.load().then(account => {
        //})

        $scope.me = me



    }
]) 