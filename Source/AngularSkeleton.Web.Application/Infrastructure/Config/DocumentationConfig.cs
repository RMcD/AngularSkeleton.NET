using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using Swashbuckle.Application;

namespace AngularSkeleton.WebApplication.Infrastructure.Config
{
    /// <summary>
    ///     Configures swashbuckle (swagger) settings.
    /// </summary>
    public class DocumentationConfig
    {
        /// <summary>
        ///     Registers the configuration.
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config
                .EnableSwagger(c =>
                {
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Bin";
                    var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                    var commentsFile = Path.Combine(baseDirectory, commentsFileName);

                    c.SingleApiVersion("v1", "Envoice API")
                        .Description("A sample API for testing and prototyping Swashbuckle features")
                        .TermsOfService("Some terms")
                        .Contact(cc => cc
                            .Name("Your name")
                            .Url("http://www.yourdomain.com/contact")
                            .Email("info@yourdomain.com"))
                        .License(lc => lc
                            .Name("License")
                            .Url("http://www.yourdomain.com/license"));

                    c.IncludeXmlComments(commentsFile);

                    c
                        .OAuth2("oauth2")
                        .Description("Resource owner password credentials grant")
                        .Flow("password")
                        .AuthorizationUrl("http://localhost:32576/api/rest/v1/accesstoken");
                })
                .EnableSwaggerUi("api/rest/docs/{*assetPath}");
        }
    }
}