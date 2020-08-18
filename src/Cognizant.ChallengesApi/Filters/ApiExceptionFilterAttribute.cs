//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;

//namespace Cognizant.Filters
//{
//    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
//    {
//        public override void OnException(ExceptionContext context)
//        {
//            if (context.Exception == null)
//            {
//                return;
//            }

//            TopLevelError error;

//            if (context.Exception is DomainException domainException)
//            {
//                //TODO: customize exception handling
//                var statusCode = HttpStatusCode.InternalServerError;
//                context.HttpContext.Response.StatusCode = (int)statusCode;
//                error = Http_1_1.GetErr(1, statusCode, "1", "Server Domain Error", domainException);
//            }
//            else if (context.Exception is InfrastructureException infrastructureException)
//            {
//                //TODO: customize exception handling
//                var statusCode = HttpStatusCode.ServiceUnavailable;
//                context.HttpContext.Response.StatusCode = (int)statusCode;
//                error = Http_1_1.GetErr(1, statusCode, "1", "Server Infrastructure Error", infrastructureException);
//            }
//            else
//            {
//                var statusCode = HttpStatusCode.InternalServerError;
//                context.HttpContext.Response.StatusCode = (int)statusCode;
//                error = Http_1_1.GetErr(1, statusCode, "1", "Server Error", context.Exception);
//            }


//            context.Result = new JsonResult(error);
//            context.ExceptionHandled = true;
//        }
//    }
//}
