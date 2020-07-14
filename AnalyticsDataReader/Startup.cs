using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AnalyticsDataReader.DAL.Model;
using AnalyticsDataReader.DAL.Repository;
using AnalyticsDataReader.Domain.Queries.Data;
using MediatR;
using System.Reflection;

namespace AnalyticsDataReader
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // WebAPI Config
            services.AddControllers();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc().AddXmlSerializerFormatters();

            services.AddMediatR(typeof(GetDataAllQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetDataRangeQueryHandler).GetTypeInfo().Assembly);
            
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AnalyticalDataConnection")));
            services.AddScoped<IAnalyticalDataRepository, AnalyticalDataRepository>();
            services.AddScoped<IAnalyticalMetaDataRepository, AnalyticalMetaDataRepository>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action}/{id?}",
                    defaults: new { controller = "AnalyticalData", action = "GetAnalyticalDataAll" });
            });
        }
    }
}
