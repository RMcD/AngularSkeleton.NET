﻿//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================


// ****************************************************************************
// Module app.manage
//

var m = angular.module('app.manage', [
    'app.manage.products',
    'app.manage.users'
])

// ****************************************************************************
// Module app.manage configuration
//

m.config(['$urlRouterProvider', '$stateProvider',
    ($urlRouterProvider: ng.ui.IUrlRouterProvider, $stateProvider: ng.ui.IStateProvider) => {

        $urlRouterProvider.when('/manage', '/manage/products')

        $stateProvider
            .state('app.manage', {
                abstract: false,
                url: '/manage',
                data: {
                    roles: ['user']
                }
            })
    }
])
