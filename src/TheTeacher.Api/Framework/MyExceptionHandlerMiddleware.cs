using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TheTeacher.Core.Exceptions;
using TheTeacher.Infrastructure.Exceptions;

namespace TheTeacher.Api.Framework
{
    public class MyExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public MyExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try 
            {
                // TODO Chaaaaaaaaaange bad practice probably seperate customeware only for dev env
                context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                
                await _next(context);
            }
            catch(Exception e)
            {
                await HandleExceptionAsync(e, context);
            }
        }

        private Task HandleExceptionAsync(Exception exception, HttpContext context)
        {
            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();
            
            switch(exception)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    break;

                case DomainException e when exceptionType == typeof(DomainException):
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = e.Code;
                    break;

                case ServiceException e when exceptionType == typeof(ServiceException):
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = e.Code;
                    break;

                case TheTeacherException e when exceptionType == typeof(TheTeacherException):
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = e.Code;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new { code = errorCode, message = exception.Message}; // TODO not the best idea though
            var payload = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}