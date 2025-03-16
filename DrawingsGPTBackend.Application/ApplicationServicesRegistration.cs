using DrawingsGPTBackend.Application.UseCases.FitViews;
using DrawingsGPTBackend.Application.UseCases.MakePartNumbers;
using DrawingsGPTBackend.Application.UseCases.PlaceDimensions;
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


            services.AddTransient<DimensionsInteractor>();
            services.AddTransient<CommonDimensionPlacer>();
            services.AddTransient<PartNumberDirectInteractor>();
            services.AddTransient<NumbersGetter>();
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Ping).Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
