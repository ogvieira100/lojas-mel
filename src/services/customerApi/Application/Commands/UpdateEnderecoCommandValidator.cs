using buildingBlocksCore.Models;
using buildingBlocksCore.Utils;
using FluentValidation;

namespace customerApi.Application.Commands
{
    public class UpdateEnderecoCommandValidator : AbstractValidator<UpdateEnderecoCommand>
    {

        public UpdateEnderecoCommandValidator()
        {
            RuleFor(x => x.Estado)
                .Must((uf) => ((int)uf).EValidoEnum<UF>())
                .WithMessage("Atenção Uf inválida");
            RuleFor(x => x.TipoEndereco)
             .Must((tipoEndereco) => ((int)tipoEndereco).EValidoEnum<TipoEndereco>())
             .WithMessage("Atenção tipo endereço inválida");
            RuleFor(x => x.Numero).NotEmpty()
                .WithMessage("Atenção preencha o número")
                .Must(ValidarNumero)
                .WithMessage("Atenção número tem que sem menor igual a 10");
            RuleFor(x => x.Logradouro)
                .Length(3, 200)
                .WithMessage(" Atenção logradouro inválido. deve estar entre 3 e 200 caracteres ");
            RuleFor(x => x.Bairro)
                .Length(3, 50)
                .WithMessage($"Atenção o bairro deve estar entre 3 e 50 caracteres");
        }

        bool ValidarNumero(string Numero)
        => !string.IsNullOrEmpty(Numero)
           && Numero.Length <= 10;

        
            
    }
}
