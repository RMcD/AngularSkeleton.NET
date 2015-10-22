﻿using System;
using System.IO;
using System.Web.Http;
using Swashbuckle.Application;

namespace AngularSkeleton.Web.Application.Infrastructure.Config
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
                    // core assembly comments
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Bin";
                    c.IncludeXmlComments(Path.Combine(baseDirectory, "AngularSkeleton.Web.Application.xml"));
                    c.IncludeXmlComments(Path.Combine(baseDirectory, "AngularSkeleton.Service.Model.xml"));

                    // models assembly comments

                    c.SingleApiVersion("v1", "AngularSkeleton.NET")
                        .Description("Angular skeleton application API")
                        .TermsOfService("Some terms")
                        .Contact(cc => cc
                            .Name("Christopher Town")
                            .Url("https://github.com/christophla/AngularSkeleton.NET"))
                        .License(lc => lc
                            .Name("License")
                            .Url("https://github.com/christophla/AngularSkeleton.NET/blob/master/LICENSE"));

                    c
                        .OAuth2("oauth2")
                        .Description("Resource owner password credentials grant")
                        .Flow("password")
                        .AuthorizationUrl("http://localhost:54391/api/rest/v1/accesstoken");
                })
                .EnableSwaggerUi("api/rest/docs/{*assetPath}");
        }
    }
}