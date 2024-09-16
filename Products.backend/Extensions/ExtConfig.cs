using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.backend.Data;
using Products.backend.ExceptionHdl;
using Products.backend.Repo;
using Products.backend.Repo.IRepo;
using Serilog;
using System.Reflection;

namespace Products.backend.Extensions
{
    public static class ExtConfig
    {
        public static void AddConfigExt(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context,LoggerConfig) =>
            {
                LoggerConfig.ReadFrom.Configuration(context.Configuration);
            });
            //builder.Services.AddMediatR(c=>c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            builder.Services.AddMediatR(c=>c.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        }

        public static void AddRepoExt(this IServiceCollection services)
        {
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICategoryRepo,CategoryRepo>();
        }
        public static void AddDbExt(this WebApplicationBuilder builder) 
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon"));
            });
        }
    }
}
