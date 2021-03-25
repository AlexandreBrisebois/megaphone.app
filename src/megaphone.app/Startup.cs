using Megaphone.App.Data;
using Megaphone.Standard.Extensions;
using Megaphone.Standard.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;

namespace Megaphone.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddHttpClient();

            if (Debugger.IsAttached)
            {
                if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("API-URL")))
                {
                    var clock = new DateTimeOffset(2021, 3, 15, 0, 0, 0, TimeSpan.Zero).ToFixedClock();

                    services.AddSingleton<IClock>(clock);
                    services.AddSingleton<IFeedService, MockFeedService>();
                    services.AddSingleton<IResourceService, MockResourceService>();
                }
                else
                {
                    services.AddSingleton<IClock, UtcClock>();
                    services.AddSingleton<IFeedService, HttpFeedService>();
                    services.AddSingleton<IResourceService, HttpResourceService>();
                }
            }
            else
            {
                services.AddSingleton<IClock, UtcClock>();
                services.AddSingleton<IFeedService, DaprFeedService>();
                services.AddSingleton<IResourceService, DaprResourceService>();
            }

            string key = Environment.GetEnvironmentVariable("INSTRUMENTATION_KEY");
            if (!string.IsNullOrEmpty(key))
                services.AddApplicationInsightsTelemetry(key);
            else
                services.AddApplicationInsightsTelemetry();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCloudEvents();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
