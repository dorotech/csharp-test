using BackendTest.Extensions;

namespace BackendTest.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "388dd3a277cb44c4aa128b5c899a3106";
                o.LogId = new Guid("c468b2b8-b35d-4f1a-849d-f47b60eef096");
            });

            services.AddHealthChecks()
                .AddElmahIoPublisher(options =>
                {
                    options.ApiKey = "388dd3a277cb44c4aa128b5c899a3106";
                    options.LogId = new Guid("c468b2b8-b35d-4f1a-849d-f47b60eef096");
                    options.HeartbeatId = "API Fornecedores";

                })
                .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            services.AddHealthChecksUI()
                .AddSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}