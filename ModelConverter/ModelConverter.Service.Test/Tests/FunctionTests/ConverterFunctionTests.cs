using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.DTOs.Responses;
using ModelConverter.Common.Services;
using ModelConverter.Common.Services.Interfaces;
using ModelConverter.Service.Functions;
using ModelConverter.Service.Services.Interfaces;
using ModelConverter.Service.Test.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Service.Test.Tests.FunctionTests
{
    public class ConverterFunctionTests
    {
        private ConverterFunction function;
        private IConverterService converterServiceMock;
        private ILogger<ConverterFunction> loggerMock;
        private IValidator<ModelConvertingRequest> validatorMock;
        private IExceptionHandler exceptionHandlerMock;
        private HttpRequestMockProvider requestMockProvider;

        [SetUp]
        public void SetUp()
        {
            converterServiceMock = Mock.Of<IConverterService>();
            loggerMock = Mock.Of<ILogger<ConverterFunction>>();
            validatorMock = Mock.Of<IValidator<ModelConvertingRequest>>();
            exceptionHandlerMock = new ExceptionHandler(Mock.Of<ILogger<ExceptionHandler>>());
            requestMockProvider = new HttpRequestMockProvider();
            function = new ConverterFunction(converterServiceMock, loggerMock, validatorMock, exceptionHandlerMock);
        }

        [Test]
        public async Task RunFunction_StartConvertingSuccessfuly_ReturnOkobjectResult()
        {
            //Arrange
            var requestBody = new ModelConvertingRequest()
            {
                ProcessId = "someId",
                InputPath = "c:",
                OutputPath = "c:",
                TargetFormat = Common.Enums.TargetFormat.Obj
            };

            requestMockProvider.SetRequestBody(requestBody);

            var request = requestMockProvider.httpRequest;

            //Act
            var response = await function.Run(request);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
            var okObjectResult = (OkObjectResult)response;
            Assert.IsNotNull(okObjectResult);
            var convertingResponse = okObjectResult.Value as ModelConvertingResponse;
            Assert.AreEqual(requestBody.ProcessId, convertingResponse.ProcessId);
        }
    }
}