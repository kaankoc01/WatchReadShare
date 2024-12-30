using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Application.Features.Movies.Create;

namespace WatchReadShare.Application.Features.Genres.Create
{
    public class CreateGenreRequestValidator : AbstractValidator<CreateGenreRequest>
    {
        private readonly IGenreRepository _genreRepository;

        public CreateGenreRequestValidator(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tür ismi Gereklidir.")
                .MinimumLength(3).WithMessage("Tür ismi en az 3 karakter olmalıdır.");

        }
    }
}
