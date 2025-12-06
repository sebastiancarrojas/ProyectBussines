using Microsoft.EntityFrameworkCore;
using PersonalProyect.Data;
using PersonalProyect.Services.Abtractions;
using PersonalProyect.Services.Implementations;

namespace PersonalProyect
{
    public static class CustomConfiguration
    {
        public static WebApplicationBuilder ConfigureCustomSettings(this WebApplicationBuilder builder)
        {
            // Carga la cadena de conexión desde appsettings.json
            string? connectionString = builder.Configuration.GetConnectionString("MyConnection");
            // Configura el DbContext con la cadena de conexión
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));
            // Registra los servicios personalizados
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ISaleDetail, SaleDetailService>();
            builder.Services.AddScoped<ISaleService, SaleService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7172/"); // tu API
            });

            // Agrega AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Agrega controladores
            builder.Services.AddControllers();

            // Custom configuration settings can be added here in the future
            return builder;
        }
    }
}
