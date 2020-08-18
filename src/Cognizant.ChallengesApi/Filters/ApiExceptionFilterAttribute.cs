using Cognizant.Domain;
using Cognizant.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Cognizant.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception == null)
            {
                return;
            }

            string error;

            if (context.Exception is DomainException domainException)
            {
                //TODO: customize exception handling
                var statusCode = HttpStatusCode.InternalServerError;
                context.HttpContext.Response.StatusCode = (int)statusCode;
                error = "Server Domain Error";
            }
            else if (context.Exception is InfrastructureException infrastructureException)
            {
                //TODO: customize exception handling
                var statusCode = HttpStatusCode.ServiceUnavailable;
                context.HttpContext.Response.StatusCode = (int)statusCode;
                error = "Server Infrastructure Error";
            }
            else
            {
                var statusCode = HttpStatusCode.InternalServerError;
                context.HttpContext.Response.StatusCode = (int)statusCode;
                error = "Server Error";
            }


            context.Result = new JsonResult(error);
            context.ExceptionHandled = true;
        }
    }
}
