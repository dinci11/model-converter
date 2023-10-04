using FluentValidation;
using ModelConverter.Common.DTOs.Requestes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Validators
{
    public class ModelConvertingRequestValidator : AbstractValidator<ModelConvertingRequest>
    {
        public ModelConvertingRequestValidator()
        {
            RuleFor(request => request.ProcessId)
                .NotEmpty()
                .WithMessage("ProcessId should be defined"); ;

            RuleFor(request => request.OutputPath)
                .NotEmpty()
                .WithMessage("OutputPath should be defined"); ;

            RuleFor(request => request.InputPath)
                .NotEmpty()
                .WithMessage("InputPath should be defined"); ;

            RuleFor(request => request.TargetFormat)
                .NotNull()
                .IsInEnum()
                .WithMessage("TargetFromat should be defined");
        }
    }
}