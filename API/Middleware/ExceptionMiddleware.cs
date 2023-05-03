using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        public RequestDelegate _next { get; }
        private readonly ILogger<ExceptionMiddleware> _logger;
        public IHostEnvironment _Env { get; set; }
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment env )
        {
            this._Env = env;
            this._logger = logger;
            this._next = next;
            
        }

        public async Task InvokeAsync(HttpContext context)
        {
        try 
        {
            await _next(context);
        }catch (Exception ex)
        {
        _logger.LogError(ex,ex.Message);
        context.Response.ContentType="application/json";
        context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
        var response =_Env.IsDevelopment()?
        new ApiException((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
        :new ApiResponse((int) HttpStatusCode.InternalServerError);
        var json =JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
        }
        }
    }
}