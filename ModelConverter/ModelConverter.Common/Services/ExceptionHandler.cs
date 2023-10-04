using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelConverter.Common.Exceptions;
using ModelConverter.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Services
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;
        private readonly Dictionary<Type, Func<Exception, Task<IActionResult>>> _exceptionHandlers;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _exceptionHandlers = new Dictionary<Type, Func<Exception, Task<IActionResult>>>
            {
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(BadRequestException), HandleBadRequestException },
                { typeof(ProcessFailedException), HandleProcessFailedException },
                { typeof(ValidationException), HandleValidationException }
            };

            _logger = logger;
        }

        private async Task<IActionResult> HandleValidationException(Exception exception)
        {
            try
            {
                var validationException = exception as ValidationException;
                var response = new BadRequestObjectResult(new
                {
                    message = validationException.Message,
                });

                return response;
            }
            catch (Exception ex)
            {
                return await HandleDefaultException(ex);
            }
        }

        private async Task<IActionResult> HandleProcessFailedException(Exception exception)
        {
            try
            {
                var processFailedException = exception as ProcessFailedException;
                var response = new ObjectResult(new
                {
                    processId = processFailedException.ProcessId,
                    message = processFailedException.Message
                });

                return response;
            }
            catch (Exception ex)
            {
                return await HandleDefaultException(ex);
            }
        }

        private async Task<IActionResult> HandleBadRequestException(Exception exception)
        {
            try
            {
                var badRequestException = exception as BadRequestException;
                var response = new BadRequestObjectResult(new
                {
                    message = badRequestException.Message
                });

                return response;
            }
            catch (Exception ex)
            {
                return await HandleDefaultException(ex);
            }
        }

        private async Task<IActionResult> HandleNotFoundException(Exception ex)
        {
            try
            {
                var notFoundException = ex as NotFoundException;
                return new NotFoundObjectResult(new
                {
                    message = notFoundException.Message
                });
            }
            catch (Exception)
            {
                return await HandleDefaultException(ex);
            }
        }

        private async Task<IActionResult> HandleDefaultException(Exception exception)
        {
            _logger.LogWarning("Exception forwarded to default handler");
            var response = new ObjectResult(exception.Message);
            response.StatusCode = 500;
            return response;
        }

        public async Task<IActionResult> HandleException(Exception exception)
        {
            var exceptionType = exception.GetType();
            _logger.LogInformation($"{exceptionType.Name} thrown by application!");
            if (IsThereHandlerForException(exceptionType))
            {
                _logger.LogInformation($"Exception can be handled by registred handler");
                return await _exceptionHandlers[exceptionType].Invoke(exception);
            }
            else
            {
                _logger.LogWarning($"Exception can NOT be handled by registred handler");
                return await HandleDefaultException(exception);
            }
        }

        private bool IsThereHandlerForException(Type exceptionType)
        {
            return _exceptionHandlers.ContainsKey(exceptionType);
        }
    }
}