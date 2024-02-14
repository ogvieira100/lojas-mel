using FluentValidation;
using productApi.Application.Commands.Products;

namespace productApi.Application.Commands.Validation
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Descricao)
             .NotEmpty()
             .WithMessage("Atenção produto deve ter descrição")
             .Length(3, 200)
             .WithMessage("Atenção produtos deve ter no minimo três caracteres e no máximo 200")
    
             ;

            RuleFor(x => x.Id)
                .Must(pr => pr == Guid.Empty)
                .WithMessage("Atenção id tem que ter valor");
        }
    }
}
