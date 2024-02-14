using FluentValidation;
using productApi.Application.Commands.Products;

namespace productApi.Application.Commands.Validation
{
    public class InsertProductCommandValidator : AbstractValidator<InsertProductCommand>
    {
        public InsertProductCommandValidator()
        {
            RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage("Atenção produto deve ter descrição")
            .Length(3,200)
            .WithMessage("Atenção produtos deve ter no minimo três caracteres e no máximo 200")
            ;
        }
    }
}
