namespace MicroService.Template.ApiService.DependencyInjection
{
    public static class ConfigureTools
    {
        public static TBuilder AddTools<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(MicroService.Template.Modules.IModule).Assembly);
            });

            return builder;
        }
    }
}
