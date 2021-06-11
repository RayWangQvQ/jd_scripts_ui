using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Ray.JdScriptsUi.OpenApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ray.JdScriptsUi.OpenApi", Version = "v1" });
            });

            services.AddDirectoryBrowser();//注册文件目录服务

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ray.JdScriptsUi.OpenApi v1"));
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            IFileProvider fileProvider = new PhysicalFileProvider(@"G:\DockerContainers\jd_scripts\jd_scripts_bak");
            var contentTypeProvider = new FileExtensionContentTypeProvider();
            contentTypeProvider.Mappings[".js"] = "text/plain";
            contentTypeProvider.Mappings.Add(".log", "text/plain");
            app.UseStaticFiles();

            app.UseDirectoryBrowser();

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = fileProvider,
                RequestPath = "/scripts",
                EnableDirectoryBrowsing = true
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
