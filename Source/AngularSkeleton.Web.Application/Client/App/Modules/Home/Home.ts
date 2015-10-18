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
// Module app.home
//
 
var m = angular.module('app.home', [])
 

// ****************************************************************************
// Configure app.home
//

m.config(['$stateProvider', 'settings', ($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) => {

    $stateProvider
        .state('app.home', {
            url: '/home',
            data: {
                roles: ['user']
            },
            controller: 'app.home',
            views: {
                'content@': {
                    templateUrl: `${settings.moduleBaseUri}/home/home.tpl.html`,
                    controller: 'app.home'
                }
            },
            resolve: {
                me: ['services', (services: IServices) => services.profile.me()]
            }
        })
}])


// ****************************************************************************
// Controller app.home
//

interface IHomeScope {
    fullName: string
}

m.controller('app.home', ['$scope', 'me', 'repositories', 'services',
    ($scope: IHomeScope, me: IUser, repositories: IRepositories, services: IServices) => {

        services.logger.debug('Loaded controller app.home')
        
        $scope.fullName = me.fullName()
    }
]) 