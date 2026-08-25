using FluentValidation;
using Ixp.Interview.Api.Models;

namespace Ixp.Interview.Api.Validators;

public sealed class CreateFaultRequestValidator : AbstractValidator<CreateFaultRequest>
{
    public CreateFaultRequestValidator()
    {
        RuleFor(x => x.Category).NotEmpty();
        RuleFor(x => x.Area).NotEmpty();
        RuleFor(x => x.Location).NotEmpty();
        RuleFor(x => x.Description);
    }
}
