using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace Jibble
{
    
    public class HttpResponseException : Exception
    {
        public HttpResponseException(HttpStatusCode statusCode, object value = null) : this((int)statusCode, value)
        {

        }
        public HttpResponseException(int statusCode, object value = null) =>
            (StatusCode, Value) = (statusCode, value);

        public int StatusCode { get; }

        public object Value { get; }
    }
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(exception.Value)
                {
                    StatusCode = exception.StatusCode,
                };
                context.ExceptionHandled = true;
            }
        }
    }

}
