using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelConverter.Common.Exceptions;
using ModelConverter.Common.Services;
using ModelConverter.Common.Services.Interfaces;
using ModelConverter.Common.Test.Helpers.TestData;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Test.Tests.ServiceTests
{
    public class ExceptionHandlerTests : TestBase<ExceptionHandler>
    {
        private IExceptionHandler exceptionHandler;

        private static readonly TestCaseData[] expectedResponseForException = new TestCaseData[]
        {
            new TestCaseData(new BadRequestException("Some message"), new BadRequestObjectResult("Some message")),
            new TestCaseData(new NotFoundException("Some message"), new NotFoundObjectResult("Some message")),
            new TestCaseData(new ValidationException("Some message"), new BadRequestObjectResult("Some message")),
            new TestCaseData(new ProcessFailedException("someProcessId","Some message"), new ObjectResult("Some message"){  StatusCode = 500}),
            new TestCaseData(new TestException(), new ObjectResult("Some message"){  StatusCode = 500}),
            new TestCaseData(new Exception("Some message"), new ObjectResult("Some message"){  StatusCode = 500}),
        };

        [SetUp]
        public void SetUp()

        {
            exceptionHandler = new ExceptionHandler(logger);
        }

        [Test, TestCaseSource(nameof(expectedResponseForException))]
        public async Task HandleException_ReturnWithProperResponseTimeAsync(Exception ex, IActionResult expectedResponse)
        {
            //Arrange

            //Act
            var response = await exceptionHandler.HandleExceptionAsync(ex);

            //Assert
            Assert.IsInstanceOf(expectedResponse.GetType(), response);
        }
    }
}