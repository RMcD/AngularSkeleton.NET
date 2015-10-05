using System;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http;
using System.Web.Http.Filters;
using NLog;

namespace AngularSkeleton.NET.WebApplication.Infrastructure.Attributes
{
    /// <summary>
    ///     Handles exception to http status code mapping.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    internal class ExceptionHandling : ExceptionFilterAttribute
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Executes when an exception occurs.
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            // Security Exception
            if (context.Exception is SecurityException)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "Security"
                });

            // Not Found Exception
            if (context.Exception is NotFoundException)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "NotFound"
                });

            // Business Exception
            if (context.Exception is BusinessException)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = "BadRequest"
                });

            //_logger.Debug(context.Exception);

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An error occurred, please try again or contact the administrator."),
                ReasonPhrase = "Critical Exception"
            });
        }
    }
}