using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderBook.Services;

namespace OrderBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Default Controllers
            services.AddControllers();

            // Log4Net
            services.AddScoped(factory => LogManager.GetLogger(GetType()));

            // Custom Service
            services.AddSingleton<OrderService>();

            // Redis Service
            services.AddMemoryCache();
            services.AddStackExchangeRedisCache(options =>
                options.Configuration = "localhost:6379");
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ILoggerFactory loggerFactory
        )
        {
            // Default Config
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            // Log4Net Config
            loggerFactory.AddLog4Net();
        }
    }
}
