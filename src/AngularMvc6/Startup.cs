using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;

using AngularMvc6.Services;

namespace AngularMvc6
{
    public class Startup
    {
        private IApplicationEnvironment _appEnv;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<MvcOptions>(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            });

            services.AddSingleton(_ => Configuration);
            services.AddSingleton<StopsServiceOptions>();
            services.AddSingleton<IStopsService, StopsService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapWebApiRoute(
                    name: "webApiDefault",
                    template: "{controller=Stops}/{action=Stops}");
            });
        }

        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            _appEnv = appEnv;

            var configurationBuilder = new ConfigurationBuilder(_appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();
        }
    }
}
