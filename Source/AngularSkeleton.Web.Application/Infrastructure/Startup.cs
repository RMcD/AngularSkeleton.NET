//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System;
using System.Web.Http;
using AngularSkeleton.NET.WebApplication.Infrastructure.Config;
using AngularSkeleton.WebApplication.Infrastructure.Config;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace AngularSkeleton.WebApplication.Infrastructure
{
    /// <summary>
    ///     Application (OWIN) Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Configures the application
        /// </summary>
        /// <param name="app">The app builder</param>
        /// <param name="serviceFacade">An instance of the service facade ofr mocking unit tests</param>
        public void Configuration(IAppBuilder app, IServiceFacade serviceFacade)
        {
            ConfigureOAuth(app);

            var config = new HttpConfiguration();
            ApiRouteConfig.Register(config);
            FormattersConfig.Register(config);
            HandlersConfig.Register(config);
            MapperConfig.Initialize();
            ContainerConfig.Register(config, serviceFacade);
            DocumentationConfig.Register(config);
            
            // authorize all requests
            config.Filters.Add(new AuthorizeAttribute());

            app.UseWebApi(config);

            // cors
            app.UseCors(CorsOptions.AllowAll);

            // entity view cache
            EntityContext.EnableViewCache();
        }

        /// <summary>
        ///     Configures the application
        /// </summary>
        /// <param name="app">The app builder</param>
        public void Configuration(IAppBuilder app)
        {
            Configuration(app, null);
        }

        private static void ConfigureOAuth(IAppBuilder app)
        {
            var serverOptions = new OAuthAuthorizationServerOptions
            {
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true,
                Provider = new MultiTenantAuthorizationServerProvider(),
                TokenEndpointPath = new PathString($"/{Constants.Api.Version.RestV1RoutePrefix}/accesstoken") // absolute for oauth
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(serverOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}