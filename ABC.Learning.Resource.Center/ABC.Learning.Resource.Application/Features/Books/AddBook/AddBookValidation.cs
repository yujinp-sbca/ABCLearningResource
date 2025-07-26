using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class AddBookValidation : AbstractValidator<AddBookRequestDTO>
    {
        public AddBookValidation()
        {
            RuleFor(p => p)
                .NotNull()
                    .WithMessage("Add book request parameter cannot be null")
                .DependentRules(() =>
                {
                    RuleFor(a => a.Title)
                        .NotEmpty()
                            .WithMessage("AddBookRequestDTO.Title cannot be empty.")
                        .NotNull()
                            .WithMessage("AddBookRequestDTO.Title cannot be null.");

                    RuleFor(a => a.ISBN)
                        .NotEmpty()
                            .WithMessage("AddBookRequestDTO.ISBN cannot be empty.")
                        .NotNull()
                            .WithMessage("AddBookRequestDTO.ISBN cannot be null.");

                    RuleFor(a => a.Author)
                        .NotEmpty()
                            .WithMessage("AddBookRequestDTO.Author cannot be empty.")
                        .NotNull()
                            .WithMessage("AddBookRequestDTO.Author cannot be null.");

                    RuleFor(a => a.Publisher)
                        .NotEmpty()
                            .WithMessage("AddBookRequestDTO.Publisher cannot be empty.")
                        .NotNull()
                            .WithMessage("AddBookRequestDTO.Publisher cannot be null.");

                    RuleFor(a => a.Description)
                        .NotEmpty()
                            .WithMessage("AddBookRequestDTO.Description cannot be empty.")
                        .NotNull()
                            .WithMessage("AddBookRequestDTO.Description cannot be null.");

                    RuleFor(a => a.PublishedDate)                        
                        .NotNull()
                            .WithMessage("AddBookRequestDTO.PublishedDate cannot be null.");

                    RuleFor(a => a.Abstract)
                        .NotEmpty()
                            .WithMessage("AddBookRequestDTO.Abstract cannot be empty.")
                        .NotNull()
                            .WithMessage("AddBookRequestDTO.Abstract cannot be null.");

                    RuleFor(a => a.CategoryId)
                        .NotEqual(Guid.Empty)
                            .WithMessage("AddBookRequestDTO.CategoryId cannot be empty.")
                        .NotNull()
                            .WithMessage("AddBookRequestDTO.CategoryId cannot be null.");

                    RuleFor(a => a.CreatedBy)
                        .NotEmpty()
                            .WithMessage("AddBookRequestDTO.CreatedBy cannot be empty.")
                        .NotNull()
                            .WithMessage("AddBookRequestDTO.CreatedBy cannot be null.");
                });
        }
    }
}
