using AccountApi.Domain.Enums;
using AccountApi.Dto_s;
using FluentValidation;

namespace AccountApi.Application.Validators
{
    public class RegisterUserValidator : AbstractValidator<UserForRegisterDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty().MinimumLength(6);

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Geçersiz rol seçimi.");
        }
    }
    public class ClaimValidator : AbstractValidator<ClaimDto>
    {
        public ClaimValidator()
        {
            RuleFor(c => c.Role).IsInEnum();

        }
    }
}
