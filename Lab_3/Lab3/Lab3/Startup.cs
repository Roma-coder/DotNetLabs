using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lab3
{
    public class Startup
    {
        private HtmlWriter htmlWriter;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            string connectionString = Configuration.GetConnectionString("StudentPerfomance");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            ApplicationContext applicationContext = new ApplicationContext(dbContextOptionsBuilder.Options);

            htmlWriter = new HtmlWriter(applicationContext);

            services.AddRazorPages();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async (httpContext) => {
                    await htmlWriter.WriteMainPage(httpContext);
                });

                endpoints.MapGet("/students", async (httpContext) => {
                    await htmlWriter.WriteStudentsPage(httpContext);
                });

                endpoints.MapGet("/teachers", async (httpContext) => {
                    await htmlWriter.WriteTeachersPage(httpContext);
                });

                endpoints.MapGet("/subjects", async (httpContext) => {
                    await htmlWriter.WriteSubjectsPage(httpContext);
                });
            });
        }
    }
}
