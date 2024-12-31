using FluentValidation;
using WatchReadShare.Application.Contracts.Persistence;

namespace WatchReadShare.Application.Features.Comments.Create
{
    public class CreateCommentRequestValidator : AbstractValidator<CreateCommentRequest>
    {
        private readonly ICommentRepository _commentRepository;

        public CreateCommentRequestValidator(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Yorum içeriği gereklidir.")
                .MinimumLength(2).WithMessage("Yorum içeriği en az 2 karakter olmalıdır.");

        }
    }
}
