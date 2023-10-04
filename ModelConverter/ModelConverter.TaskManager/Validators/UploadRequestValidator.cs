using FluentValidation;
using ModelConverter.TaskManager.DTOs;

namespace ModelConverter.TaskManager.Validators
{
    public class UploadRequestValidator : AbstractValidator<UploadRequest>
    {
        public UploadRequestValidator()
        {
            RuleFor(request => request.TargetFormat)
                .NotNull()
                .IsInEnum()
                .WithMessage("TargetFormat should be declared");
        }
    }
}