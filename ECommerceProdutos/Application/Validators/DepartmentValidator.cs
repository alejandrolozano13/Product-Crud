using Domain.Entities;
using FluentValidation;

namespace Application.Validators.DepartmentValidator
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(d => d.Code)
                .NotEmpty()
                .WithMessage("O código do departamento não pode ser nulo");

            RuleFor(d => d.Description)
                .NotEmpty()
                .WithMessage("A descrição do departamento é obrigatória")
                .MaximumLength(255)
                .WithMessage("A descrição deve ter no máximo 255 caracteres."); ;
        }
    }
}