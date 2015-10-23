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
// Interfaces app.security.principal
//

interface IIdentity {
    name: string
    token: string
    roles: string[]
}

interface IPrincipal {
    authenticate(identity: IIdentity): void
    configure(config: IPrincipalConfiguration): void
    isAuthenticated(): boolean
    identity(force?: boolean): angular.IPromise<IIdentity>
    isIdentityResolved(): boolean
    isInAnyRole(roles: string[]): boolean
    isInRole(role: string): boolean
}

enum PrincipalStorageMode { Local, Session }

interface IPrincipalConfiguration {
    requestTokenName: string
    storageKey: string
    storageMode: PrincipalStorageMode
}


// ****************************************************************************
// Module app.security.principal
//

var m = angular.module('common.security.principal', ['common.services.underscore'])


// ****************************************************************************
// Factory app.security.principal 
//

m.factory('principal', ['$http', '$q', '$timeout', '_', ($http: ng.IHttpService, $q, $timeout, underscore: UnderscoreStatic) => {

    var _identity: IIdentity = undefined
    var _authenticated = false
    var _loginState = ''
    var _storageKey = 'principal.identity'
    var _storageMode = PrincipalStorageMode.Session
    var _requestTokenName = 'Authorization'

    const storage = (_storageMode === PrincipalStorageMode.Local) ? localStorage : sessionStorage;

    function configure(config: IPrincipalConfiguration) {
        _requestTokenName = config.requestTokenName
        _storageKey = config.storageKey
        _storageMode = config.storageMode
    }

    function authenticate(identity: IIdentity) {
        _identity = identity
        _authenticated = identity != null

        if (identity) {
            storage.setItem(_storageKey, angular.toJson(_identity))
            $http.defaults.headers.common[_requestTokenName] = `bearer ${identity.token}`
        } else {
            storage.removeItem(_storageKey)
            $http.defaults.headers.common[_requestTokenName] = undefined
        }
    }

    function identity(force?: boolean) {
        const deferred = $q.defer();

        if (force) _identity = undefined

        // check and see if we have retrieved the identity data from the server. if we have, reuse it by immediately resolving
        if (angular.isDefined(_identity)) {
            deferred.resolve(_identity)
            return deferred.promise
        }

        // retrieve identity from storage 

        //$timeout(() => {
            switch (_storageMode) {
                case PrincipalStorageMode.Local:
                    _identity = angular.fromJson(localStorage.getItem(_storageKey))
                    break;
                case PrincipalStorageMode.Session:
                    _identity = angular.fromJson(sessionStorage.getItem(_storageKey))
                    break;
            }
            this.authenticate(_identity)
            deferred.resolve(_identity)
        //}, 100)

        return deferred.promise
    }

    function isAuthenticated() {
        return _authenticated
    }

    function isIdentityResolved() {
        return angular.isDefined(_identity);
    }

    function isInRole(role: string) {
        if (!_authenticated || !_identity.roles) return false;

        return underscore.indexOf(_identity.roles, role) !== -1
        //return _identity.roles.indexOf(role) != -1;
    }

    function isInAnyRole(roles: string[]) {
        if (!_authenticated || !_identity.roles) return false;
        for (var i = 0; i < roles.length; i++) {
            if (this.isInRole(roles[i])) return true;
        }

        return false;
    }
    
    var principal: IPrincipal = {
        authenticate: authenticate,
        configure: configure,
        identity: identity,
        isAuthenticated: isAuthenticated,
        isIdentityResolved: isIdentityResolved,
        isInAnyRole: isInAnyRole,
        isInRole: isInRole
    }

    return principal
}])
