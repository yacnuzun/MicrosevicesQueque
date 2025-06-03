using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Enums;
using AccountApi.Dto_s;
using FluentValidation;

namespace AccountApi.Application.Validators
{
    public class RegisterUserValidator : AbstractValidator<UserForRegisterDto>
    {
        private readonly IUserService _userService;
        public RegisterUserValidator(IUserService _userService)
        {
            RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.");

            RuleFor(x => x)
            .MustAsync(async (dto, cancellation) =>
            {
                var user = await _userService.GetExistUser(dto.UserTaxId, dto.Email, dto.UserName);
                return !user.Success||user.Data is null?true:false;
            });

            RuleFor(x => x.UserTaxId)
                .NotEmpty()
                .Length(10, 11)
                .WithMessage("Vergi numarası 10 veya 11 haneli olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş bırakılamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Geçersiz rol seçimi.");

        }
    }
}
