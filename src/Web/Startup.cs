using Application;
using Application.Mappings;
using AutoMapper;
using Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using VueCliMiddleware;
using Web.Middleware;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase();
            services.AddApplication();
            services.AddAutoMapper(typeof(TransactionProfile).Assembly);

            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddCors(
                options =>
                    options.AddPolicy(
                        "AllowAllPolicy",
                        builder =>
                            builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                    )
            );

            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(
                        "v1", new OpenApiInfo
                        {
                            Title = "Accounting API",
                            Version = "v1"
                        }
                    );
                }
            );
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSpaStaticFiles(
                configuration =>
                {
                    configuration.RootPath = "client-app/dist";
                }
            );
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AllowAllPolicy");
            app.UseCustomExceptionHandler();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(
                setup =>
                {
                    setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Accounting API");
                    setup.DocumentTitle = "Accounting API";
                }
            );

            app.UseEndpoints(
                endpoints => endpoints.MapControllers()
            );

            app.UseSpa(
                spa =>
                {
                    spa.Options.SourcePath = "client-app";

                    if (env.IsDevelopment())
                    {
                        spa.UseVueCli();
                        spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                    }
                }
            );
        }
    }
}
