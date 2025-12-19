using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.Data.Seeders;
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
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IBrandService, BrandService>();

            // MasterSeeder
            builder.Services.AddTransient<MasterSeeder>();

            // API
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7172/"); // tu API
            });

            // Agrega AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Identity and Access Management
            AddIAM(builder);
            // Configura Notyf para notificaciones
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.BottomRight;
            });
            // Agrega controladores
            builder.Services.AddControllers();

            // Custom configuration settings can be added here in the future
            return builder;
        }

        private static void AddIAM(WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<User, IdentityRole<Guid>>(conf =>

            {
                conf.User.RequireUniqueEmail = true;
                conf.Password.RequireDigit = false;
                conf.Password.RequiredUniqueChars = 0;
                conf.Password.RequireLowercase = false;
                conf.Password.RequireUppercase = false;
                conf.Password.RequireNonAlphanumeric = false;
                conf.Password.RequiredLength = 4;
            }).AddEntityFrameworkStores<DataContext>()
              .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(conf =>
            {
                conf.Cookie.Name = "Auth";
                conf.ExpireTimeSpan = TimeSpan.FromDays(100);
                conf.LoginPath = "/Account/Login";
                conf.AccessDeniedPath = "/Error/403";
            });
        }
    }
}
