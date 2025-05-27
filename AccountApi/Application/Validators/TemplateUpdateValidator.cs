using AccountApi.Dto_s;
using FluentValidation;

namespace AccountApi.Application.Validators
{
    public class TemplateUpdateValidator : AbstractValidator<TemplateUpdateDto>
    {
        public TemplateUpdateValidator()
        {
            RuleFor(t => t.TemplateId)
            .GreaterThan(0)
            .WithMessage("Geçerli bir şablon ID giriniz.");

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
