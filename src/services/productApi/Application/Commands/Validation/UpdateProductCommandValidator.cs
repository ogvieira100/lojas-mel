using FluentValidation;
using productApi.Application.Commands.Products;

namespace productApi.Application.Commands.Validation
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
                
        }
    }
}
