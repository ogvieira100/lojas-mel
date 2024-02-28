using buildingBlocksCore.Utils;
using FluentValidation;
using supplierApi.Application.Commands.Supplier;

namespace supplierApi.Application.Commands.Validation
{

    public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierCommandValidator()
        {

            RuleFor(x => x.Id)
                .Must((id) => id == Guid.Empty)
                .WithMessage(" Atenção Id Inválido ");

            RuleFor(x => x.CNPJ)
                .Must((cnpj) => cnpj.IsCnpj())
                .WithMessage(" Atenção CNPJ inválido ");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(" Atenção email inválido ");

            RuleFor(x => x.RazaoSocial)
                .Must((razao) =>
                {
                    if (string.IsNullOrEmpty(razao) || (razao.Length > 0 && razao.Length >= 40 && razao.Length <= 3 ))
                        return true;
                    return false;
                })
                .WithMessage(" Atenção campo Razão Social Inválido. ");
        }
    }
}
