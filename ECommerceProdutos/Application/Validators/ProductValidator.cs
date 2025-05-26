using Domain.Entities;
using FluentValidation;

namespace Application.Validators.ProductValidator
{
    public class ProductValidator: AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty()
                .WithMessage("O código do produto é obrigatório.")
                .MaximumLength(50)
                .WithMessage("O código deve ter no máximo 50 caracteres.");

            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("A descrição é obrigatória.")
                .MaximumLength(255)
                .WithMessage("A descrição deve ter no máximo 255 caracteres.");

            RuleFor(p => p.DepartmentCode)
                .NotEmpty()
                .WithMessage("O código do departamento é obrigatório.");

            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("O preço do produto deve ser maior que zero");

            RuleFor(p => p.Active)
                .NotNull()
                .WithMessage("O status de ativo deve ser informado.");
        }
    }
}