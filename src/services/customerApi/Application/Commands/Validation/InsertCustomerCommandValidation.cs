using buildingBlocksCore.Utils;
using customerApi.Application.Commands.Customer;
using FluentValidation;

namespace customerApi.Application.Commands.Validation
{
    public class InsertCustomerCommandValidation : AbstractValidator<InsertCustomerCommand>
    {
        public InsertCustomerCommandValidation()
        {
            RuleFor(x => x.CPF)
                .NotEmpty()
                .WithMessage("Atenção CPF deve ser preenchido.")
                .Must((cpf)=> cpf.IsCpf())
                .WithMessage($"Atenção CPF inválido.")
                ;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Atenção e-mail deve ser preenchido")
                .Must((email)=> email.IsValidEmail())
                .WithMessage("Atenção email inválido.")
                ;

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Atenção nome deve ser preenchido.")
                .Length(3, 200)
                .WithMessage("Atenção nome está fora do range de 3 a 200")
                ;
                

        }
    }
}
