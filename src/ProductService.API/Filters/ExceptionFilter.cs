using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ProductMicroservice.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProductMicroservice.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                default:
                    _logger.LogError(context.Exception, "Unexpected exception");
                    context.Result = new ObjectResult(new ErrorResponse { ErrorMessage = "An unexpected error was encountered" })
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                    break;
            }
        }
    }
}
