using AccountApi.Dto_s;
using FluentValidation;

namespace AccountApi.Application.Validators
{
    public class TemplateAddValidator : AbstractValidator<TemplateAddDto>
    {
        public TemplateAddValidator()
        {
            RuleFor(t => t.TemplateCode)
            .NotEmpty()
            .WithMessage("Tasarım kodu boş olamaz.")
            .IsInEnum()
            .WithMessage("Geçersiz tasarım kodu seçimi.");

            RuleFor(t => t.Subject)
                .NotEmpty()
                .WithMessage("Konu alanı boş olamaz.")
                .MinimumLength(6)
                .WithMessage("Konu en az 6 karakter olmalıdır.");

            RuleFor(t => t.Body)
                .NotEmpty()
                .WithMessage("İçerik alanı boş olamaz.")
                .MinimumLength(6)
                .WithMessage("İçerik en az 6 karakter olmalıdır.");
        }
    }
}
