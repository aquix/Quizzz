using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Quizzz.Util.Exceptions
{
    public class InvalidParameterExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var result = new ContentResult();
            result.Content = context.Exception.Message;
            result.StatusCode = (int)HttpStatusCode.BadRequest;
            context.ExceptionHandled = true; // mark exception as handled
            context.Result = result;
            base.OnException(context);
        }
    }
}