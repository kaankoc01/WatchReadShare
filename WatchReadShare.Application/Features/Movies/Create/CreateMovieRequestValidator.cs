using FluentValidation;
using WatchReadShare.Application.Contracts.Persistence;

namespace WatchReadShare.Application.Features.Movies.Create
{
    public class CreateMovieRequestValidator : AbstractValidator<CreateMovieRequest>
    {
        private readonly IMovieRepository _movieRepository;

        public CreateMovieRequestValidator(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Film ismi Gereklidir.")
                .MinimumLength(2).WithMessage("Film ismi en az 2 karakter olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Film Açıklaması Gereklidir.")
                .MaximumLength(500).WithMessage("Film Açıklaması En fazla 500 karakter olmalıdır.");

            RuleFor(x => x.GenreId).GreaterThan(0).WithMessage("Ürün Tür değeri 0 dan büyük olmalıdır.");

        }

    }
}
