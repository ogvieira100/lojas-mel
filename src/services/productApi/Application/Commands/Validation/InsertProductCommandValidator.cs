using FluentValidation;
using productApi.Application.Commands.Products;

namespace productApi.Application.Commands.Validation
{
    public class InsertProductCommandValidator : AbstractValidator<InsertProductCommand>
    {
        public InsertProductCommandValidator()
        {
                
        }
    }
}
