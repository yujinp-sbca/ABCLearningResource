using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class UpdateBookValidation : AbstractValidator<UpdateBookRequestDTO>
    {
        public UpdateBookValidation()
        {
            RuleFor(p => p)
                .NotNull()
                    .WithMessage("Update book request parameter cannot be null")
                .DependentRules(() =>
                {
                    RuleFor(a => a.BookId)
                        .NotEqual(Guid.Empty)
                            .WithMessage("UpdateBookRequestDTO.BookId cannot be empty.")
                        .NotNull()
                            .WithMessage("UpdateBookRequestDTO.BookId cannot be null.");

                    RuleFor(a => a.Title)
                        .NotEmpty()
                            .WithMessage("UpdateBookRequestDTO.Title cannot be empty.")
                        .NotNull()
                            .WithMessage("UpdateBookRequestDTO.Title cannot be null.");

                    RuleFor(a => a.ISBN)
                        .NotEmpty()
                            .WithMessage("UpdateBookRequestDTO.ISBN cannot be empty.")
                        .NotNull()
                            .WithMessage("UpdateBookRequestDTO.ISBN cannot be null.");

                    RuleFor(a => a.Author)
                        .NotEmpty()
                            .WithMessage("UpdateBookRequestDTO.Author cannot be empty.")
                        .NotNull()
                            .WithMessage("UpdateBookRequestDTO.Author cannot be null.");

                    RuleFor(a => a.Publisher)
                        .NotEmpty()
                            .WithMessage("UpdateBookRequestDTO.Publisher cannot be empty.")
                        .NotNull()
                            .WithMessage("UpdateBookRequestDTO.Publisher cannot be null.");

                    RuleFor(a => a.Description)
                        .NotEmpty()
                            .WithMessage("UpdateBookRequestDTO.Description cannot be empty.")
                        .NotNull()
                            .WithMessage("UpdateBookRequestDTO.Description cannot be null.");

                    RuleFor(a => a.PublishedDate)
                        .NotNull()
                            .WithMessage("UpdateBookRequestDTO.PublishedDate cannot be null.");

                    RuleFor(a => a.Abstract)
                        .NotEmpty()
                            .WithMessage("UpdateBookRequestDTO.Abstract cannot be empty.")
                        .NotNull()
                            .WithMessage("UpdateBookRequestDTO.Abstract cannot be null.");

                    RuleFor(a => a.CategoryId)
                        .NotEqual(Guid.Empty)
                            .WithMessage("UpdateBookRequestDTO.CategoryId cannot be empty.")
                        .NotNull()
                            .WithMessage("UpdateBookRequestDTO.CategoryId cannot be null.");

                    RuleFor(a => a.ModifiedBy)
                        .NotEmpty()
                            .WithMessage("UpdateBookRequestDTO.ModifiedBy cannot be empty.")
                        .NotNull()
                            .WithMessage("UpdateBookRequestDTO.ModifiedBy cannot be null.");
                });
        }
    }
}
