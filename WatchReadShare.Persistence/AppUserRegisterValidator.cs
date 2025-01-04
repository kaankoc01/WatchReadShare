using FluentValidation;
using WatchReadShare.Application.Features.Auth;

namespace WatchReadShare.Persistence
{
    public class AppUserRegisterValidator : AbstractValidator<RegisterDto>
    {
        public AppUserRegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş bırakılamaz.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad alanı boş bırakılamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta alanı boş bırakılamaz.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı alanı boş geçilemez.");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("En fazla 30 karakter veri girişi yapın.");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("En az 2 karakter veri girişi yapın.");

        }
    }
}
