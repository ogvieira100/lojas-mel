using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Validations.Extension
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services, Type assemblyContainingValidators)
        {
            try
            {
                services.AddFluentValidation(assemblyContainingValidators);

                return services;
            }
            catch (Exception ex)
            {

                throw;
            }

            
        }

        public static List<MessageResult>? GetErrors(this ValidationResult result)
        {
            return result.Errors?.Select(error => new MessageResult(error.PropertyName, error.ErrorMessage)).ToList();
        }
    }
}
