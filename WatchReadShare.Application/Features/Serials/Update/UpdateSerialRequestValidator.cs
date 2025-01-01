
using FluentValidation;
using WatchReadShare.Application.Contracts.Persistence;

namespace WatchReadShare.Application.Features.Serials.Update
{
    public class UpdateSerialRequestValidator : AbstractValidator<UpdateSerialRequest>
    {
        private readonly ISerialRepository _serialRepository;

        public UpdateSerialRequestValidator(ISerialRepository serialRepository)
        {
            _serialRepository = serialRepository;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Dizi ismi Gereklidir.")
                .MinimumLength(2).WithMessage("Dizi ismi en az 2 karakter olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Dizi Açıklaması Gereklidir.")
                .MaximumLength(500).WithMessage("Dizi Açıklaması En fazla 500 karakter olmalıdır.");

            RuleFor(x => x.GenreId).GreaterThan(0).WithMessage("Ürün Tür değeri 0 dan büyük olmalıdır.");
        }
    }
}
