using FluentValidation;
using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Validators
{
    public class StatusUpdateRequestValidator : AbstractValidator<StatusUpdateRequest>
    {
        public StatusUpdateRequestValidator()
        {
            RuleFor(request => request.Status)
                .NotNull()
                .IsInEnum()
                .WithMessage("Status must be declared!");

            RuleFor(request => request.OutputPath)
                .NotEmpty()
                .WithMessage("OutputPath must be declared!");

            RuleFor(request => request.ProcessId)
                .NotEmpty()
                .WithMessage("ProcessId must be declared!");
        }
    }
}