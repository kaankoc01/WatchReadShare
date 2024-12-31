using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchReadShare.Application.Contracts.Persistence;

namespace WatchReadShare.Application.Features.Comments.Update
{
    public class UpdateCommentRequestValidator : AbstractValidator<UpdateCommentRequest>
    {
        private readonly ICommentRepository _commentRepository;

        public UpdateCommentRequestValidator(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Yorum içeriği gereklidir.")
                .MinimumLength(2).WithMessage("Yorum içeriği en az 2 karakter olmalıdır.");

        }
    }
}
