using AccountApi.Dto_s;
using FluentValidation;

namespace AccountApi.Application.Validators
{
    public class ClaimValidator : AbstractValidator<ClaimDto>
    {
        public ClaimValidator()
        {
            RuleFor(c => c.Role).IsInEnum();

        }
    }
}
