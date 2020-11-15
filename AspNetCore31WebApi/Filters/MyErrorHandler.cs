
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCore31WebApi.Filters
{
    public class MyErrorHandler : IExceptionFilter
    {
        private readonly ILogger<MyErrorHandler> _logger;

        public MyErrorHandler(ILogger<MyErrorHandler> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            string message = $"An exception with message {context.Exception.Message} was thrown.";
            _logger.LogError(message);
        }
    }
}
