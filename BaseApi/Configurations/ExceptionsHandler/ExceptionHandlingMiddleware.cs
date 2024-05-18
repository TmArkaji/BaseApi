using BaseApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace BaseApi.Configurations.ExceptionsHandler
{

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly EmailService _emailService;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env, EmailService emailService)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env;
            _emailService = emailService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task<Task> HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                ValidationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var result = new
            {
                StatusCode = response.StatusCode,
                Message = exception.Message, 
                Errors = (exception as ValidationException)?.Errors,

                Detailed = (_env.IsDevelopment()) ? exception.StackTrace : ""
            };

            if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                var user = context.User.Identity.IsAuthenticated ? context.User.Identity.Name.ToString() : "";
                var url = context.Request.GetDisplayUrl();
                var stackTrace = exception.StackTrace == null ? "No exception.StackTrace" : exception.StackTrace.ToString();

                await _emailService.SendEmailAsync("Exception occurred",
                    DateTime.Now + "<br/><br/>" +
                    user + "<br/><br/>" +
                    url + "<br/><br/>" +
                    exception.ToString() + "<br/><br/>" +
                    stackTrace
                );
                await _emailService.SendEmailAsync("Unhandled Exception", $"{exception.Message}\n\n{exception.StackTrace}");
            }

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
        }
    }

}
