using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;

namespace Quizzz.Util.Exceptions
{
    public class InvalidParameterExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is InvalidParameterException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                context.Response.Content = new StringContent(context.Exception.Message);
            }
        }
    }
}