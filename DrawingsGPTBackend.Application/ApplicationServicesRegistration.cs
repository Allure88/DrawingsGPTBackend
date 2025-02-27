using DrawingsGPTBackend.Application.UseCases.FitViews;
using Microsoft.Extensions.DependencyInjection;

namespace DrawingsGPTBackend.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ViewsInteractor>();
            services.AddTransient<OrientationHandler>();
            services.AddTransient<ScaleFormatHandler>();
            services.AddTransient<ViewsSettler>();
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Ping).Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
