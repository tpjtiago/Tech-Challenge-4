using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Application.Services;
using Tech.Challenge4.Data.Contexts;
using Tech.Challenge4.Data.Repositories;
using Tech.Challenge4.Domain.Contracts.Services.Coworkings;
using Tech.Challenge4.Domain.Contracts.Services.Customers;
using Tech.Challenge4.Domain.Contracts.Services.Payments;
using Tech.Challenge4.Domain.Contracts.Services.Reservas;
using Tech.Challenge4.Domain.Contracts.Services.Salas;
using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Common.Extensions
{
    public static class DependencyInjectionsExtension
    {
        public static void AddIoC(this IServiceCollection services, IConfiguration configuration)
        {
            //Register Mappings
            services.AddAutoMapper(typeof(Customer).Assembly);
            services.AddAutoMapper(typeof(Coworking).Assembly);
            services.AddAutoMapper(typeof(Sala).Assembly);
            services.AddAutoMapper(typeof(Reserva).Assembly);

            //Register Services
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICoworkingService, CoworkingService>();
            services.AddScoped<ISalaService, SalaService>();
            services.AddScoped<IReservaService, ReservaService>();
            services.AddScoped<IPaymentsService, PaymentService>();

            //Register Repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICoworkingRepository, CoworkingRepository>();
            services.AddScoped<ISalaRepository, SalaRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();

            // Register SQL Server Database
            services.AddDbContext<CoworkingContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CoworkingConnection"));
            });
        }
    }
}
