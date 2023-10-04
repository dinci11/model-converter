using FluentValidation;
using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Test.Tests.ValidatorTests
{
    public class ModelConvertingRequestValidatorTests
    {
        private IValidator<ModelConvertingRequest> validator;

        private static TestCaseData[] ValidRequests = new TestCaseData[]
        {
            new TestCaseData(new ModelConvertingRequest
            {
                InputPath = "C:/Input",
                OutputPath = "C:/Output",
                ProcessId = Guid.NewGuid().ToString(),
                TargetFormat = Enums.TargetFormat.Obj
            }),
            new TestCaseData(new ModelConvertingRequest
            {
                InputPath = "C:",
                OutputPath = "C:/Output",
                ProcessId = Guid.NewGuid().ToString(),
                TargetFormat = Enums.TargetFormat.Step
            }),
            new TestCaseData(new ModelConvertingRequest
            {
                InputPath = "C:/Input",
                OutputPath = "C:",
                ProcessId = Guid.NewGuid().ToString(),
                TargetFormat = Enums.TargetFormat.Stl
            }),
            new TestCaseData(new ModelConvertingRequest
            {
                InputPath = "C:/Input",
                OutputPath = "C:/Output",
                ProcessId = Guid.NewGuid().ToString(),
                TargetFormat = Enums.TargetFormat.Iges
            }),
        };

        private static TestCaseData[] InValidRequests = new TestCaseData[]
{
            new TestCaseData(new ModelConvertingRequest
            {
                InputPath = "",
                OutputPath = "C:/Output",
                ProcessId = Guid.NewGuid().ToString(),
                TargetFormat = Enums.TargetFormat.Obj
            }),
            new TestCaseData(new ModelConvertingRequest
            {
                InputPath = "C:",
                OutputPath = "",
                ProcessId = Guid.NewGuid().ToString(),
                TargetFormat = Enums.TargetFormat.Step
            }),
            new TestCaseData(new ModelConvertingRequest
            {
                InputPath = "C:/Input",
                OutputPath = "C:",
                ProcessId = null,
                TargetFormat = Enums.TargetFormat.Stl
            }),
            new TestCaseData(new ModelConvertingRequest
            {
                InputPath = "C:/Input",
                OutputPath = "C:/Output",
                ProcessId = Guid.NewGuid().ToString(),
                TargetFormat = null
            }),
};

        [OneTimeSetUp]
        public void SetUp()
        {
            validator = new ModelConvertingRequestValidator();
        }

        [Test, TestCaseSource(nameof(ValidRequests))]
        public void Validate_ValidRequests_NoExceptionThrown(ModelConvertingRequest request)
        {
            //Assert
            Assert.DoesNotThrow(() => validator.Validate(request));
        }

        [Test, TestCaseSource(nameof(InValidRequests))]
        public void Validate_InValidRequests_ExceptionShouldBeThrown(ModelConvertingRequest request)
        {
            //Assert
            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));
        }
    }
}