using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome é obrigatório");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O e-mail é obrigatório")
                .EmailAddress()
                .WithMessage("O e-mail informado não é válido");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("A senha é obrigatória")
                .MinimumLength(6)
                .WithMessage("A senha deve ter pelo menos 6 caracteres");
        }
    }
}