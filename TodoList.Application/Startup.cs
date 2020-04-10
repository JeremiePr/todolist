using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoList.Application.Data;

namespace TodoList.Application
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<HttpClient>();

            services.AddSingleton(new AppSettings(
                baseApiUrl: Configuration.GetValue<string>("ExtUrls:WebApi")
            ));

            foreach (var (contract, implementation) in ServiceTypes)
            {
                services.AddSingleton(contract ?? implementation, implementation);
            }
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

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private IEnumerable<(Type contract, Type implementation)> ServiceTypes => Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t =>
                t.Namespace != null &&
                t.Namespace.StartsWith(GetType().Namespace + ".Services") &&
                t.Name.EndsWith("Service") &&
                t.IsClass || (t.IsInterface && t.Name.StartsWith("I")))
            .Select(t => new
            {
                type = t,
                typeName = t.IsInterface ? t.Name.Substring(1) : t.Name
            })
            .GroupBy(x => x.typeName)
            .Where(g => g.Any(x => x.type.IsClass))
            .Select(g => (contract: g.SingleOrDefault(x => x.type.IsInterface)?.type, implementation: g.Single(x => x.type.IsClass).type));
    }
}
