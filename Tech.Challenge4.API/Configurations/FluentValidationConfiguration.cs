using FluentValidation;
using Tech.Challenge4.Domain.Validators;

namespace Tech.Challenge4.API.Configurations
{
    public static class FluentValidationConfiguration
    {
        public static void AddFluentValidation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<CustomerValidator>();
            services.AddValidatorsFromAssemblyContaining<CoworkingValidator>();
            services.AddValidatorsFromAssemblyContaining<SalaValidator>();
        }
    }
}
